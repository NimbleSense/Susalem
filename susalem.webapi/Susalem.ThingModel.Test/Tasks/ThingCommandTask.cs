using Newtonsoft.Json;
using Quartz;
using Susalem.Infrastructure.ThingModel;
using Susalem.Infrastructure.ThingModel.Interface;
using Susalem.Infrastructure.ThingModel.Model;
using Susalem.Messages.Features.Channel;
using Susalem.ThingModel.Test.MobudsThing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.ThingModel.Test.Tasks
{
    [PersistJobDataAfterExecution, DisallowConcurrentExecution]
    public class ThingCommandTask : IJob
    {
        public ThingCommandTask()
        {
            foreach(var item in Appsession.DictReadIoModels)
            {
                string key = item.Key;
                IModbusThingDriver modbusThingDriver = Appsession.MonitorDrivers[key];
                for (int i = 0;i<item.Value.Count;i++)
                {
                    var ioModel = item.Value[i];
                    if(ioModel.PropertyKeys.Length==1)
                    {
                        Property pro = Appsession.Devices[key].Properties.Find(x=>x.Key==ioModel.PropertyKeys[0]);
                        pro.CurrentValue = modbusThingDriver.Read(ioModel, false).Value;
                    }
                    else
                    {
                        ModbusThingRetModel model =  modbusThingDriver.Read(ioModel, true);
                        ushort[] value = (ushort[])model.Value;
                        // 多地址混合读取
                        for (int j=0;j< ioModel.PropertyKeys.Length;j++)
                        {
                            Property pro = Appsession.Devices[key].Properties.Find(x => x.Key == ioModel.PropertyKeys[j]);
                            if(j==0)
                            {
                                ushort[] newValue = new ushort[ioModel.BatchLength[j]];
                                Array.Copy(value, 0, newValue, 0, ioModel.BatchLength[j]);
                                pro.CurrentValue = ReturnValue(ioModel.ValueType, newValue);

                            }
                            else
                            {
                                ushort[] newValue = new ushort[value[ioModel.BatchLength[j]]];
                                Array.Copy(value, ioModel.BatchLength[j], newValue, 0, value[ioModel.BatchLength[j]]);
                                pro.CurrentValue = ReturnValue(ioModel.ValueType, newValue);
                            }
                        }
                    }
                }
            }
        }
        public async Task Execute(IJobExecutionContext context)
        {
            //for (int i = 0; i < Appsession.ThingCommands.Count; i++)
            //{

            //}
        }

        private object ReturnValue(DataTypeEnum dataType, ushort[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            switch (dataType)
            {
                // 布尔类型 (1个ushort)
                case DataTypeEnum.Bit:
                case DataTypeEnum.Bool:
                    if (value.Length < 1) throw new ArgumentException("Insufficient data length");
                    return value[0] != 0;

                // 单字节类型 (取低8位)
                case DataTypeEnum.UByte:
                    if (value.Length < 1) throw new ArgumentException("Insufficient data length");
                    return (byte)(value[0] & 0xFF);
                case DataTypeEnum.Byte:
                    if (value.Length < 1) throw new ArgumentException("Insufficient data length");
                    return (sbyte)(value[0] & 0xFF);

                // 16位整数类型 (直接转换)
                case DataTypeEnum.Uint16:
                    if (value.Length < 1) throw new ArgumentException("Insufficient data length");
                    return value[0];
                case DataTypeEnum.Int16:
                    if (value.Length < 1) throw new ArgumentException("Insufficient data length");
                    return (short)value[0];

                // BCD 编码转换 (16位)
                case DataTypeEnum.Bcd16:
                    if (value.Length < 1) throw new ArgumentException("Insufficient data length");
                    return ConvertBcd16(value[0]);

                // 32位类型 (组合2个ushort)
                case DataTypeEnum.Uint32:
                    if (value.Length < 2) throw new ArgumentException("Insufficient data length");
                    return BitConverter.ToUInt32(GetBytes(value, 2, true), 0);
                case DataTypeEnum.Int32:
                    if (value.Length < 2) throw new ArgumentException("Insufficient data length");
                    return BitConverter.ToInt32(GetBytes(value, 2, true), 0);
                case DataTypeEnum.Float:
                    if (value.Length < 2) throw new ArgumentException("Insufficient data length");
                    return BitConverter.ToSingle(GetBytes(value, 2, true), 0);
                case DataTypeEnum.Bcd32:
                    if (value.Length < 2) throw new ArgumentException("Insufficient data length");
                    return ConvertBcd32(value[0], value[1]);

                // 64位类型 (组合4个ushort)
                case DataTypeEnum.Uint64:
                    if (value.Length < 4) throw new ArgumentException("Insufficient data length");
                    return BitConverter.ToUInt64(GetBytes(value, 4, true), 0);
                case DataTypeEnum.Int64:
                    if (value.Length < 4) throw new ArgumentException("Insufficient data length");
                    return BitConverter.ToInt64(GetBytes(value, 4, true), 0);
                case DataTypeEnum.Double:
                    if (value.Length < 4) throw new ArgumentException("Insufficient data length");
                    return BitConverter.ToDouble(GetBytes(value, 4, true), 0);

                // 字符串类型 (所有字节拼接)
                case DataTypeEnum.AsciiString:
                    return Encoding.ASCII.GetString(GetBytes(value, value.Length, false));
                case DataTypeEnum.Utf8String:
                    return Encoding.UTF8.GetString(GetBytes(value, value.Length, false));
                case DataTypeEnum.Gb2312String:
                    return Encoding.GetEncoding("GB2312").GetString(GetBytes(value, value.Length, false));

                // 日期时间类型 (6字节自定义格式)
                case DataTypeEnum.DateTime:
                    if (value.Length < 3) throw new ArgumentException("Insufficient data length");
                    return new DateTime(
                        year: value[0],
                        month: (byte)(value[1] & 0xFF),
                        day: (byte)(value[1] >> 8),
                        hour: (byte)(value[2] & 0xFF),
                        minute: (byte)(value[2] >> 8),
                        second: 0
                    );

                // 时间戳类型 (4字节整数)
                case DataTypeEnum.TimeStampMs:
                    if (value.Length < 2) throw new ArgumentException("Insufficient data length");
                    uint ms = BitConverter.ToUInt32(GetBytes(value, 2, true), 0);
                    return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(ms);
                case DataTypeEnum.TimeStampS:
                    if (value.Length < 2) throw new ArgumentException("Insufficient data length");
                    uint sec = BitConverter.ToUInt32(GetBytes(value, 2, true), 0);
                    return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(sec);

                // 未定义类型返回原始数组
                case DataTypeEnum.Any:
                case DataTypeEnum.Custome1:
                case DataTypeEnum.Custome2:
                case DataTypeEnum.Custome3:
                case DataTypeEnum.Custome4:
                case DataTypeEnum.Custome5:
                    return value;

                default:
                    throw new NotSupportedException($"Unsupported data type: {dataType}");
            }
        }

        // 辅助方法：将ushort数组转换为字节数组
        private byte[] GetBytes(ushort[] values, int ushortCount, bool forNumeric)
        {
            int byteCount = ushortCount * 2;
            byte[] bytes = new byte[byteCount];

            for (int i = 0; i < ushortCount; i++)
            {
                bytes[i * 2] = (byte)(values[i] & 0xFF);       // 低字节
                bytes[i * 2 + 1] = (byte)(values[i] >> 8);     // 高字节
            }

            // 数值类型需要处理字节序
            if (forNumeric && !BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        // BCD16转换 (4位十进制数)
        private int ConvertBcd16(ushort bcd)
        {
            int result = 0;
            int multiplier = 1;

            for (int i = 0; i < 4; i++)
            {
                int digit = bcd & 0x000F;
                if (digit > 9) throw new ArgumentException("Invalid BCD digit");

                result += digit * multiplier;
                multiplier *= 10;
                bcd >>= 4;
            }
            return result;
        }

        // BCD32转换 (8位十进制数)
        private long ConvertBcd32(ushort low, ushort high)
        {
            long result = 0;
            long multiplier = 1;
            uint combined = (uint)((high << 16) | low);

            for (int i = 0; i < 8; i++)
            {
                int digit = (int)(combined & 0x0000000F);
                if (digit > 9) throw new ArgumentException("Invalid BCD digit");

                result += digit * multiplier;
                multiplier *= 10;
                combined >>= 4;
            }
            return result;
        }

    }
}
