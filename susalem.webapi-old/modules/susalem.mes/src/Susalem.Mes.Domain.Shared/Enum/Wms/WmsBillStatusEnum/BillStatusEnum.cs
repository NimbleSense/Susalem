using System.ComponentModel;
namespace Susalem.Wms.WmsBillStatusEnum
{
    /// <summary>
    /// 出入库记录单据状态
    /// </summary>
    public enum BillStatusEnum
    {
        /// <summary>
        /// 新建
        /// </summary>
        [Description("新建")]
        New = 0,

        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Finish = 1,

        /// <summary>
        /// 生效
        /// </summary>
        [Description("生效")]
        Effective = 2,
    }
}
