using NModbus.Utility;
using NModbus;
using System.IO.Ports;
using Microsoft.Extensions.Logging;
using Susalem.Messages.Features.Channel;
using Susalem.Core.Application.Interfaces.Services;
using NModbus.Serial;

namespace Susalem.Infrastructure.ThingModel
{
    public class ModbusRtuDeviceBase : IThingObjectDriver
    {
        private readonly SerialSetting _setting;
        private readonly ILogger _logger;
        private readonly SerialPort _serialPort;
        private IModbusSerialMaster _serialMaster;

        public bool IsConnected => _serialPort.IsOpen;

        public ModbusRtuDeviceBase(SerialSetting setting, CommonSetting commonSetting, ILogger logger)
        {
            if (setting == null) throw new ArgumentNullException(nameof(setting));
            if (commonSetting == null) throw new ArgumentNullException(nameof(commonSetting));
            _setting = setting;
            _logger = logger;

            _serialPort = new SerialPort(setting.PortName)
            {
                BaudRate = setting.BaudRate,
                DataBits = setting.DataBits,
                StopBits = (StopBits)setting.StopBits,
                Parity = (Parity)setting.Parity,
                ReadTimeout = commonSetting.ReadTimeout,
                WriteTimeout = commonSetting.WriteTimeout
            };
        }

        public bool Connect()
        {
            _logger.LogInformation($"Open {_serialPort.PortName} {_serialPort.BaudRate}");
            try
            {
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                _logger.LogError($"SerialPort open failed: {ex}");
                return false;
            }

            if (!_serialPort.IsOpen) return false;

            _serialMaster = new ModbusFactory().CreateRtuMaster(_serialPort);
            return true;
        }

        public void Disconnect()
        {
            _serialPort.Close();
        }

        public bool ExecuteReg(int address, WriteCommandDto command)
        {
            try
            {
                _serialMaster.WriteSingleRegister((byte)address, command.Reg, command.Value);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus write single register error, Com: {_setting.PortName}, Address: {address}, {e}");
                return false;
            }
        }

        public bool ExecuteRegs(int address, IList<WriteCommandDto> commands)
        {
            try
            {
                var startAddress = commands.Min(t => t.Reg);
                var data = commands.Select(t => t.Value).ToArray();
                _serialMaster.WriteMultipleRegisters((byte)address, startAddress, data);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus write multi registers error, Com: {_setting.PortName}, Address: {address}, {e}");
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

                var data = _serialMaster.ReadInputRegisters((byte)address, startReg, (ushort)length).ToList();

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
            catch (Exception e)
            {
                _logger.LogError($"Modbus read input registers error, Com: {_setting.PortName}, Address: {address}, {e}");
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

                _serialMaster.WriteMultipleRegisters((byte)address, startReg, data);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus write multi telermeties registers error, Com: {_setting.PortName}, Address: {address}, {e}");
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
                _serialMaster.WriteMultipleCoils((byte)address, startAddress, switches.ToArray());
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus write coils error, Com: {_setting.PortName}, Address: {address}, {e}");
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

                var data = _serialMaster.ReadCoils((byte)address, startReg, (ushort)length).ToList();

                //foreach (var engineTelemetry in telemetries)
                //{

                //}

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Modbus read coils error, Com: {_setting.PortName}, Address: {address}, {e}");
                return false;
            }
        }
    }
}
