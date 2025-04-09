using System.ComponentModel;
namespace Susalem.Mam.MamOrderBomTypeEnum
{
    /// <summary>
    /// 订单BOM状态枚举
    /// </summary>
    public enum OrderBomTypeEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 断点
        /// </summary>
        [Description("断点")]
        Breakpoint = 1,
    }
}
