using System.ComponentModel;
namespace Susalem.Sys
{
    /// <summary>
    /// 【枚举】补位类型（编码规则）
    /// </summary>
    public enum ComplementTypeEnum
    {
        /// <summary>
        /// 左补位
        /// </summary>
        [Description("左补位")]
        Left = 0,

        /// <summary>
        /// 右补位
        /// </summary>
        [Description("右补位")]
        Right = 1
    }
}
