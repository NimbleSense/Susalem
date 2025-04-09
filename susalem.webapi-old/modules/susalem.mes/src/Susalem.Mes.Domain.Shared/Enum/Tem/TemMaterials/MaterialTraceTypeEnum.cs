using System.ComponentModel;
namespace Susalem.Tem.TemMaterials
{
    /// <summary>
    /// 【枚举】物料追溯类型
    /// </summary>
    public enum MaterialTraceTypeEnum
    {
        /// <summary>
        /// 不追溯
        /// </summary>
        [Description("不追溯")]
        None = 0,

        /// <summary>
        /// 精追溯
        /// </summary>
        [Description("精追溯")]
        Precise =1,

        /// <summary>
        /// 批次追溯
        /// </summary>
        [Description("批次追溯")]
        Batch = 2

    }
}
