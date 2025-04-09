using System.ComponentModel;
namespace Susalem.Wms.WmsBillTypeEnum
{
    /// <summary>
    /// 出入库记录单据类型
    /// </summary>
    public enum BillTypeEnum
    {
        /// <summary>
        /// 入库
        /// </summary>
        [Description("入库")]
        Open = 0,

        /// <summary>
        /// 出库
        /// </summary>
        [Description("出库")]
        Out = 1,

        /// <summary>
        /// 调拨
        /// </summary>
        [Description("调拨")]
        Transfer = 2,
    }
}
