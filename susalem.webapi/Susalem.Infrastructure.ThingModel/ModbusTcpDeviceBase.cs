using Microsoft.Extensions.Logging;
using NModbus.Utility;
using NModbus;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Messages.Features.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.Infrastructure.ThingModel
{

    public class ModbusTcpDeviceBase : IMonitorDriver
    {
        private readonly ILogger _logger;
        private readonly TcpSetting _setting;
        private readonly CommonSetting _commonSetting;
        private IModbusMaster _tcpMaster;
        private TcpClient _tcpClient;

        public bool IsConnected
        {
            get
            {
                if (_tcpClient == null) return false;

                if (_tcpClient.Client == null) return false;

                return _tcpClient.Client.Connected;
            }
        }

        public ModbusTcpDeviceBase(TcpSetting setting, CommonSetting commonSetting, ILogger logger)
        {
            if (setting == null) throw new ArgumentNullException(nameof(setting));
            if (commonSetting == null) throw new ArgumentNullException(nameof(commonSetting));

            _logger = logger;
            _setting = setting;
            _commonSetting = commonSetting;
        }

        public bool Connect()
        {
            try
            {
                _logger.LogInformation($"Connect {_setting.Host} {_setting.Port}");

                if (_tcpClient != null)
                {
                    Disconnect();
                }

                _tcpClient = new TcpClient(_setting.Host, _setting.Port)
                {
                    ReceiveTimeout = _commonSetting.ReadTimeout,
                    SendTimeout = _commonSetting.WriteTimeout
                };

                _tcpMaster = new ModbusFactory().CreateMaster(_tcpClient);
                return _tcpClient.Client.Connected;
            }
            catch (Exception ex)
            {
                _logger.LogError("Modbus tcp client connect exception: {ex}", ex);
                return false;
            }
        }

        public void Disconnect()
        {
            _logger.LogInformation($"Connect {_setting.Host} {_setting.Port}");
            _tcpClient.Close();
            _tcpClient.Dispose();
        }

        public bool Execute(int address, EngineCommand command)
        {
            try
            {
                _tcpMaster.WriteSingleRegister((byte)address, command.Reg, command.Value);
                return true;
            }
            catch (IOException ioe)
            {
                _logger.LogError($"Modbus write single register error, Server: {_setting.Host}, Address: {address}, {ioe}");
                Disconnect();
                return false;
            }
            catch (SocketException ex) when (ex.ErrorCode == 10060)
            {
                _logger.LogError($"Modbus write single register error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus write single register error, Server: {_setting.Host}, Address: {address}, {e}");
                return false;
            }
        }

        public bool Execute(int address, IList<EngineCommand> commands)
        {
            try
            {
                var startAddress = commands.Min(t => t.Reg);
                var data = commands.Select(t => t.Value).ToArray();
                _tcpMaster.WriteMultipleRegisters((byte)address, startAddress, data);
                return true;
            }
            catch (IOException ioe)
            {
                _logger.LogError($"Modbus write multi registers error, Server: {_setting.Host}, Address: {address}, {ioe}");
                Disconnect();
                return false;
            }
            catch (SocketException ex) when (ex.ErrorCode == 10060)
            {
                _logger.LogError($"Modbus write multi registers error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus write multi registers error, Server: {_setting.Host}, Address: {address}, {e}");
                return false;
            }
        }

        public bool Read(int address, IList<EngineTelemetry> telemetries)
        {
            try
            {
                var orderedTelemetries = telemetries.OrderBy(t => t.Reg);
                var startReg = orderedTelemetries.First().Reg;

                var length = (orderedTelemetries.Last().Reg + orderedTelemetries.Last().Length) - startReg;

                var data = _tcpMaster.ReadInputRegisters((byte)address, startReg, (ushort)length).ToList();

                foreach (var engineTelemetry in telemetries)
                {
                    var telemetryData = data.GetRange(engineTelemetry.Reg - startReg, engineTelemetry.Length);
                    if (telemetryData.Count > 1)
                    {
                        engineTelemetry.Value = Math.Round(ModbusUtility.GetSingle(telemetryData[0], telemetryData[1]), 1);
                    }
                    else
                    {
                        engineTelemetry.Cal(telemetryData.First());
                    }
                }

                return true;
            }
            catch (IOException ioe)
            {
                _logger.LogError($"Modbus read input registers error, Server: {_setting.Host}, Address: {address}, {ioe}");
                //Disconnect();
                return false;
            }
            catch (SocketException ex) when (ex.ErrorCode == 10060)
            {
                _logger.LogError($"Modbus read input registers error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus read input registers error, Server: {_setting.Host}, Address: {address}, {e}");
                return false;
            }
        }

        public bool Write(int address, IList<EngineTelemetry> telemetries)
        {
            try
            {
                var orderedTelemetries = telemetries.OrderBy(t => t.Reg);
                var startReg = orderedTelemetries.First().Reg;

                var length = (orderedTelemetries.Last().Reg + orderedTelemetries.Last().Length) - startReg;
                var data = new ushort[length];

                foreach (var engineTelemetry in orderedTelemetries)
                {
                    try
                    {
                        var writeData = engineTelemetry.GetWriteData();
                        for (var i = 0; i < writeData.Length; i++)
                        {
                            data[engineTelemetry.Reg - startReg + i] = writeData[i];
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"Engine telemetry: {engineTelemetry.Key}, value:{engineTelemetry.Value} get data exception, {e}");
                    }
                }

                _tcpMaster.WriteMultipleRegisters((byte)address, startReg, data);
                return true;
            }
            catch (IOException ex)
            {
                _logger.LogError($"Modbus write multi telermeties registers error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (SocketException ex) when (ex.ErrorCode == 10060)
            {
                _logger.LogError($"Modbus write multi telermeties registers error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus write multi telermeties registers error, Server: {_setting.Host}, Address: {address}, {e}");
                return false;
            }
        }

        public bool Read(int address, IList<DoorStatus> doors)
        {
            try
            {
                var data = _tcpMaster.ReadHoldingRegisters((byte)address, 209, 4).ToList();
                for (var i = 0; i < data.Count; i++)
                {
                    doors[i].Open = data[i] == 0;
                }

                return true;
            }
            catch (IOException ex)
            {
                _logger.LogError($"Modbus read door error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (SocketException ex) when (ex.ErrorCode == 10060)
            {
                _logger.LogError($"Modbus read door error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus read door error, Server: {_setting.Host}, Address: {address}, {e}");
                return false;
            }
        }

        public bool ReadDebugData(int address, IList<DebugData> datas)
        {
            try
            {
                var orderedDatas = datas.OrderBy(t => t.Reg);
                var startReg = orderedDatas.First().Reg;

                var length = (orderedDatas.Last().Reg + orderedDatas.Last().Length) - startReg;

                var data = _tcpMaster.ReadHoldingRegisters((byte)address, startReg, (ushort)length).ToList();

                foreach (var debugData in datas)
                {
                    var telemetryData = data.GetRange(debugData.Reg - startReg, debugData.Length);
                    if (telemetryData.Count > 1)
                    {
                        debugData.Value = Math.Round(ModbusUtility.GetSingle(telemetryData[0], telemetryData[1]), 1);
                    }
                    else
                    {
                        debugData.Value = telemetryData.First();
                    }
                }

                return true;
            }
            catch (IOException ex)
            {
                _logger.LogError($"Modbus read debug data error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (SocketException ex) when (ex.ErrorCode == 10060)
            {
                _logger.LogError($"Modbus read debug data error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus read debug data error, Server: {_setting.Host}, Address: {address}, {e}");
                return false;
            }
        }

        public bool WriteDebugData(int address, DebugData data)
        {
            try
            {
                var regDatas = new ushort[data.Length];
                if (data.Length > 1)
                {
                    var writeData = BitConverter.GetBytes((float)data.Value);

                    regDatas[1] = BitConverter.ToUInt16(writeData, 0);
                    regDatas[0] = BitConverter.ToUInt16(writeData, 2);
                }
                else
                {
                    var intData = Convert.ToUInt32(data.Value);
                    var writeData = BitConverter.GetBytes(intData);

                    for (var i = 0; i < regDatas.Length; i++)
                    {
                        regDatas[i] = writeData[i];
                    }
                }

                _tcpMaster.WriteMultipleRegisters((byte)address, data.Reg, regDatas);
                return true;
            }
            catch (IOException ex)
            {
                _logger.LogError($"Modbus write debug data error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (SocketException ex) when (ex.ErrorCode == 10060)
            {
                _logger.LogError($"Modbus write debug data error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus write debug data error, Server: {_setting.Host}, Address: {address}, {e}");
                return false;
            }
        }

        public bool ExecuteCoil(int address, IList<EngineCommand> commands)
        {
            try
            {
                var startAddress = commands.Min(t => t.Reg);
                var data = commands.Select(t => t.Value).ToArray();
                var switches = new List<bool>();
                foreach (var currentData in data)
                {
                    if (currentData == 0)
                    {
                        switches.Add(false);
                    }
                    else
                    {
                        switches.Add(true);
                    }
                }
                _tcpMaster.WriteMultipleCoils((byte)address, startAddress, switches.ToArray());
                return true;
            }
            catch (IOException ex)
            {
                _logger.LogError($"Modbus write coils error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (SocketException ex) when (ex.ErrorCode == 10060)
            {
                _logger.LogError($"Modbus write coils error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus write coils error, Server: {_setting.Host}, Address: {address}, {e}");
                return false;
            }
        }

        public bool ReadCoil(int address, IList<EngineTelemetry> telemetries)
        {
            try
            {
                var orderedTelemetries = telemetries.OrderBy(t => t.Reg);
                var startReg = orderedTelemetries.First().Reg;
                var length = (orderedTelemetries.Last().Reg + orderedTelemetries.Last().Length) - startReg;

                var data = _tcpMaster.ReadCoils((byte)address, startReg, (ushort)length).ToList();
                return true;
            }
            catch (IOException ex)
            {
                _logger.LogError($"Modbus read coils error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (SocketException ex) when (ex.ErrorCode == 10060)
            {
                _logger.LogError($"Modbus read coils error, Server: {_setting.Host}, Address: {address}, {ex}");
                Disconnect();
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus read coils error, Server: {_setting.Host}, Address: {address}, {e}");
                return false;
            }
        }
    }
}
