using System.ComponentModel;
namespace Susalem.Tem.TemBarcodeRuleItems
{
    /// <summary>
    /// 条码子表类型枚举
    /// </summary>
    public enum BarcodeRuleTypeEnum
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Description("编码")]
        Code = 0,

        /// <summary>
        /// 标识
        /// </summary>
        [Description("标识")]
        Mark = 1,

        /// <summary>
        /// 日期  包括DDDYY
        /// </summary>
        [Description("日期")]
        DateTime = 2,

        /// <summary>
        /// 流水号
        /// </summary>
        [Description("标准递增流水号")]
        SerialNumber = 3,

        /// <summary>
        /// 版本
        /// </summary>
        [Description("版本")]
        Version = 4,

        /// <summary>
        /// 正则
        /// </summary>
        [Description("正则")]
        Regex = 5,

        /// <summary>
        /// 动态参数
        /// </summary>
        [Description("动态参数")]
        DyParame=6,

        /// <summary>
        /// 每日重置递增流水号
        /// </summary>
        [Description("每日重置递增流水号")]
        ReSerialNumber = 7,

        /// <summary>
        /// 电机标签
        /// </summary>
        [Description("电机标签")]
        MotorLabel = 8,

        /// <summary>
        /// 电机序号
        /// </summary>
        [Description("电机序号")]
        MotorNumber = 9,

        /// <summary>
        /// 零件码
        /// </summary>
        [Description("零件码")]
        PartCode = 10,

        /// <summary>
        /// Powerstack日期
        /// </summary>
        [Description("Powerstack日期")]
        PowerstackDate = 11,

        /// <summary>
        /// 工厂代码
        /// </summary>
        [Description("工厂代码")]
        FactoryCode = 12,

        /// <summary>
        /// 产线代码
        /// </summary>
        [Description("产线代码")]
        PdLineCode = 13,


    }
}
