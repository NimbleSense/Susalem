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

    public class ModbusTcpDeviceBase : IThingObjectDriver
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

        public bool ExecuteReg(int address, WriteCommandDto command)
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

        public bool ExecuteRegs(int address, IList<WriteCommandDto> commands)
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

        public bool ExecuteComputedRegs(int address, IList<ThingCommandDto> telemetries)
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

        public bool ReadRegs(int address, IList<ThingCommandDto> telemetries)
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

        public bool ExecuteCoils(int address, IList<ThingCommandDto> commands)
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

        public bool ReadCoils(int address, IList<ThingCommandDto> telemetries)
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
