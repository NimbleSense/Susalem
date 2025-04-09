using System.ComponentModel;
namespace Susalem.Wms.WmsCallMaterialStatusEnum
{
    /// <summary>
    /// 叫料状态
    /// </summary>
    public enum CallMaterialStatusEnum
    {
        /// <summary>
        /// 关闭
        /// </summary>
        [Description("关闭")]
        Close   = 0,
        /// <summary>
        /// 叫料中
        /// </summary>
        [Description("叫料中")]
        Calling=1,
        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Finish = 2,
    }
}
