
using System.ComponentModel;
namespace Susalem.Tem.TemMaterials
{
    /// <summary>
    /// 批次数量来源枚举
    /// </summary>
    public enum BatchQtySourceEnum
    {
        /// <summary>
        /// 条码解析
        /// </summary>
        [Description("条码解析")]
        BarCodeAnalysis = 1,

        /// <summary>
        /// 默认数量
        /// </summary>
        [Description("默认数量")]
        DefaultQty = 2
    }
}
