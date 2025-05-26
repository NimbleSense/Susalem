using MassTransit.Futures.Contracts;
using Susalem.Messages.Features.Channel;
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
    public class ThingObject
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsConnect { get; set; } = false;

        /// <summary>
        /// 设备采集协议类型
        /// </summary>
        public DeviceCollectionPro DeviceCollectionPro { get; set; }

        /// <summary>
        /// 设备连接字符串
        /// </summary>
        public string ConnectString{ get; set; }

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

        ///// <summary>
        ///// 原数据
        ///// </summary>
        //public Metadata Metadata { get; set; }

        ///// <summary>
        ///// 地址长度
        ///// </summary>
        //public int Length { get; set; }
        //public DataType DataType { get; set; }
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
        Cron = 1 ,
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
        public Trigger Trigger { get; set; }
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

        public List<CommandParameter> Parameters { get; set; }
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
       // public Transform Transform { get; set; }

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
}
