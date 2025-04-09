using System.ComponentModel;
namespace Susalem.Qms.QmsQualityStatusEnum
{
    /// <summary>
    /// 质量数据状态枚举
    /// </summary>
    public enum QualityStatusEnum
    {
        /// <summary>
        /// 异常
        /// </summary>
        [Description("异常")]
        Error = 0,
        /// <summary>
        /// 合格
        /// </summary>
        [Description("合格")]
        Ok = 1,

        /// <summary>
        /// 不合格
        /// </summary>
        [Description("不合格")]
        Ng = 2,

        /// <summary>
        /// 手动合格
        /// </summary>
        [Description("手动合格")]
        Mok = 3,
    }
}
