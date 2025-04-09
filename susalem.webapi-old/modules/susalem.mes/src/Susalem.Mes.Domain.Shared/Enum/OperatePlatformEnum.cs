using System.ComponentModel;

namespace Susalem.Mes.Enum
{
    /// <summary>
    /// 【枚举】所属平台（操作日志）
    /// </summary>
    public enum OperatePlatformEnum
    {
        /// <summary>
        /// MES管理后台
        /// </summary>
        [Description("MES管理后台")]
        Mes = 0,

        /// <summary>
        /// 数据采集程序
        /// </summary>
        [Description("数据采集程序")]
        Daq = 1,

        /// <summary>
        /// APS
        /// </summary>
        [Description("APS")]
        Aps = 2,

        /// <summary>
        /// Q-Plant
        /// </summary>
        [Description("Q-Plant")]
        QPlant = 3,

        /// <summary>
        /// Q-Plant
        /// </summary>
        [Description("LES")]
        Les = 4,

        /// <summary>
        /// Q-Plant
        /// </summary>
        [Description("RCS")]
        Rcs = 5,

        /// <summary>
        /// Q-Plant
        /// </summary>
        [Description("MOP")]
        Mop = 6,

        /// <summary>
        /// 总成码变更
        /// </summary>
        [Description("总成码变更")]
        ChangeSerialNo = 7,

        /// <summary>
        /// 条码打印记录
        /// </summary>
        [Description("条码打印记录")]
        BarcodePrint = 8,

        /// <summary>
        /// 工艺变更记录 
        /// </summary>
        [Description("工艺变更记录 ")]
        ChangeTemProduct = 9



    }
}
