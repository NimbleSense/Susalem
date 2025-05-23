using System.Collections.Generic;

namespace Susalem.Infrastructure.ThingModel
{
    public enum DeviceCollectionPro
    {
        ModbusRtu,
        ModbusTcp,
        MQTT,
        OPCUA,
        HTTP
    }

    /// <summary>
    /// 设备模型
    /// </summary>
    public class DeviceModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsConnect { get; set; } = false;

        /// <summary>
        /// 设备采集协议类型
        /// </summary>
        public DeviceCollectionPro DeviceCollectionPro { get; set; }

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
        public int Address { get; set; }

        /// <summary>
        /// 地址长度
        /// </summary>
        public int Length { get; set; }
        public DataType DataType { get; set; }

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
        public string Type { get; set; }

        /// <summary>
        /// 字节序
        /// </summary>
        public string ByteOrder { get; set; }
    }

    public class Metadata
    {
        public string Unit { get; set; }
        public int Precision { get; set; }
    }

    public class ReadConfig
    {
        public string Name { get; set; }
        public int FunctionCode { get; set; }
        public string Mode { get; set; }
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
        public string Type { get; set; }
        public int Interval { get; set; }
    }

    public class Optimization
    {
        /// <summary>
        ///  自动合并相邻地址
        /// </summary>
        public bool MergeAdjacent { get; set; }
        public string RetryPolicy { get; set; }
    }

    public class CommandConfig
    {
        public string Name { get; set; }
        public int FunctionCode { get; set; }
        public List<CommandParameter> Parameters { get; set; }
    }

    public class CommandParameter
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int Address { get; set; }
        public string ByteOrder { get; set; }
        public Transform Transform { get; set; }
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
}
