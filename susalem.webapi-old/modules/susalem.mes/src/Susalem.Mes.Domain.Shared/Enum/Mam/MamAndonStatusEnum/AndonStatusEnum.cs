using System.ComponentModel;
namespace Susalem.Mam.MamAndonStatusEnum
{
    /// <summary>
    /// Andon状态枚举
    /// </summary>
    public enum AndonStatusEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 人
        /// </summary>
        [Description("人")]
        People = 1,

        /// <summary>
        /// 机
        /// </summary>
        [Description("机")]
        Machine = 2,

        /// <summary>
        /// 料
        /// </summary>
        [Description("料")]
        Material = 3,

        /// <summary>
        /// 法
        /// </summary>
        [Description("法")]
        Method = 4,

        /// <summary>
        /// 环
        /// </summary>
        [Description("环")]
        Environment = 5
    }
}
