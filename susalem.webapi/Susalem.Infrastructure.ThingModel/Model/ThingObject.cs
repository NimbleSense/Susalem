using MassTransit.Futures.Contracts;
using Susalem.Messages.Features.Channel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Infrastructure.ThingModel.Model
{

    public enum MasterType
    {
        Tcp = 0,
        Udp = 1,
        Rtu = 2,
        RtuOnTcp = 3,
        RtuOnUdp = 4,
        Ascii = 5,
        AsciiOnTcp = 6,
        AsciiOnUdp = 7,
    }

    /// <summary>
    /// 设备模型
    /// </summary>
    public class ThingObject
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsConnect { get; set; } = false;

        /// <summary>
        /// Modbus协议类型
        /// </summary>
        public MasterType DeviceCollectionPro { get; set; }

        public TcpSetting TcpSetting { get; set; }

        public SerialSetting SerialSetting { get; set; }

        /// <summary>
        /// 通用设置
        /// </summary>
        public CommonSetting CommonSetting { get; set; }

        /// <summary>
        /// 设备的属性类型
        /// </summary>
        public List<Property> Properties { get; set; }

        /// <summary>
        /// 读配置项
        /// </summary>
        public List<ReadConfig> ReadConfigs { get; set; }

        /// <summary>
        /// 命令绑定项(写）
        /// </summary>
        public List<CommandConfig> CommandConfigs { get; set; }
    }

    public class Property
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public object CurrentValue { get; set; }


        /// <summary>
        /// 计算表达式
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 原数据
        /// </summary>
        public Metadata Metadata { get; set; }
    }

    public class DataType
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public DataTypeEnum Type { get; set; }

        /// <summary>
        /// 字节序
        /// </summary>
        public EndianEnum ByteOrder { get; set; }
    }

    public enum DataTypeEnum
    {
        [Display(Name = "bit")]
        Bit = 0,

        [Display(Name = "bool")]
        Bool = 1,

        [Display(Name = "uint8")]
        UByte = 2,

        [Display(Name = "int8")]
        Byte = 3,

        [Display(Name = "uint16")]
        Uint16 = 4,

        [Display(Name = "int16")]
        Int16 = 5,

        [Display(Name = "bcd16")]
        Bcd16 = 6,

        [Display(Name = "uint32")]
        Uint32 = 7,

        [Display(Name = "int32")]
        Int32 = 8,

        [Display(Name = "float")]
        Float = 9,

        [Display(Name = "bcd32")]
        Bcd32 = 10,

        [Display(Name = "uint64")]
        Uint64 = 11,

        [Display(Name = "int64")]
        Int64 = 12,

        [Display(Name = "double")]
        Double = 13,

        [Display(Name = "ascii")]
        AsciiString = 14,

        [Display(Name = "utf8")]
        Utf8String = 15,

        [Display(Name = "datetime")]
        DateTime = 16,

        [Display(Name = "timestamp(ms)")]
        TimeStampMs = 17,

        [Display(Name = "timestamp(s)")]
        TimeStampS = 18,

        [Display(Name = "Any")]
        Any = 19,

        [Display(Name = "Gb2312")]
        Gb2312String = 20,

        [Display(Name = "Custom1")]
        Custome1,

        [Display(Name = "Custom2")]
        Custome2,

        [Display(Name = "Custom3")]
        Custome3,

        [Display(Name = "Custom4")]
        Custome4,

        [Display(Name = "Custom5")]
        Custome5,


    }

    public enum EndianEnum
    {
        [Display(Name = "None")] None = 0,
        [Display(Name = "BigEndian")] BigEndian,
        [Display(Name = "LittleEndian")] LittleEndian,
        [Display(Name = "BigEndianSwap")] BigEndianSwap,
        [Display(Name = "LittleEndianSwap")] LittleEndianSwap
    }

    public class Metadata
    {
        public string Unit { get; set; }

        /// <summary>
        /// 精确度(小数点后几位)
        /// </summary>
        public int Precision { get; set; }
    }

    public class ReadConfig
    {
        /// <summary>
        /// 关联Properties 里的名字
        /// </summary>
        public string Name { get; set; }

        public int FunctionCode { get; set; }

        public int Length { get; set; }

        public string Address { get; set; }

        public string Mode { get; set; }

        public string Expression { get; set; }
        public Trigger Trigger { get; set; }

        /// <summary>
        /// 关联属性
        /// </summary>
        public List<string> PropertyKeys { get; set; }

        public Optimization Optimization { get; set; }


    }

    public class Trigger
    {
        /// <summary>
        /// 触发器类型
        /// </summary>
        public TriggerType Type { get; set; }
        public int Interval { get; set; }
    }

    public enum TriggerType
    {
        Interval = 0,
        Cron = 1,
        Event = 2
    }

    public class Optimization
    {
        /// <summary>
        ///  自动合并相邻地址
        /// </summary>
        public bool MergeAdjacent { get; set; }
        public string RetryPolicy { get; set; }
    }

    /// <summary>
    /// 触发式写命令
    /// </summary>
    public class CommandConfig
    {
        /// <summary>
        /// 全局唯一写入键
        /// </summary>
        public string Key { get; set; }

        public string Name { get; set; }
        //public Trigger Trigger { get; set; }
        public int FunctionCode { get; set; }

        public event EventHandler<object> OnWriteCommand
        {
            add
            {
                value.Invoke(this, Key);
                OnWriteCommand += value;
            }
            remove { OnWriteCommand -= value; }
        }

        /// <summary>
        /// 读取或者写入的长度
        /// </summary>
        public int Length { get; set; }

        public DataType DataType { get; set; }

        /// <summary>
        /// 寄存器或线圈地址
        /// </summary>
        public string Address { get; set; }

        public string Expression { get; set; }
        public Validation Validation { get; set; }
    }

    public class CommandParameter
    {
        /// <summary>
        /// 读取或者写入的长度
        /// </summary>
        public int Length { get; set; }

        public string DataType { get; set; }

        /// <summary>
        /// 寄存器或线圈地址
        /// </summary>
        public int Address { get; set; }
        public string ByteOrder { get; set; }

        public string Expression { get; set; }
        public Validation Validation { get; set; }
    }

    public class Transform
    {
        public string Expression { get; set; }
        public Validation Validation { get; set; }
    }

    public class Validation
    {
        public float Min { get; set; }
        public float Max { get; set; }
    }


    public enum VaribaleStatusTypeEnum
    {
        Good = 0,
        AddressError,
        MethodError,
        ExpressionError,
        Bad,
        UnKnow,
        Custome1,
        Custome2,
        Custome3,
        Custome4,
        Custome5
    }
}
