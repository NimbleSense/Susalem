using System.ComponentModel;
namespace Susalem.Wms.WmsCallMaterialTypeEnum
{
    /// <summary>
    /// 叫料类型
    /// </summary>
    public enum CallMaterialTypeEnum
    {
        /// <summary>
        /// LES库房
        /// </summary>
        [Description("LES库房")]
        Les = 0,

        /// <summary>
        /// 清洗线
        /// </summary>
        [Description("清洗线")]
        wash = 1
    }
}
