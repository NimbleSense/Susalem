using System.ComponentModel;

namespace Susalem.Ecm.EcmStructItemEnum
{
    /// <summary>
    /// 能耗类型枚举
    /// </summary>
    public enum StructItemEnergyTypeEnum
    {
        ///// <summary>
        ///// 水
        ///// </summary>
        //[Description("水")]
        //Water = 0,

        /// <summary>
        /// 电
        /// </summary>
        [Description("电")]
        Electricity = 1,

        /// <summary>
        /// 压缩空气
        /// </summary>
        [Description("压缩空气")]
        Air = 2,

        /// <summary>
        /// 温度
        /// </summary>
        [Description("温度")]
        Temp = 3,

        /// <summary>
        /// 湿度
        /// </summary>
        [Description("湿度")]
        Hum = 4,
    }
}
