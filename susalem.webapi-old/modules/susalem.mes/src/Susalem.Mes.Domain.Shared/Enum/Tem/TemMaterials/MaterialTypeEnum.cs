using System.ComponentModel;
namespace Susalem.Tem.TemMaterials
{
    /// <summary>
    /// 【枚举】 物料类型
    /// </summary>
    public enum MaterialTypeEnum
    {
        /// <summary>
        /// 原材料
        /// </summary>
        [Description("原材料")]
        Raw = 0,

        /// <summary>
        /// 半成品
        /// </summary>
        [Description("半成品")]
        Partially = 1,

        /// <summary>
        /// 成品
        /// </summary>
        [Description("成品")]
        Product = 2
    }
}
