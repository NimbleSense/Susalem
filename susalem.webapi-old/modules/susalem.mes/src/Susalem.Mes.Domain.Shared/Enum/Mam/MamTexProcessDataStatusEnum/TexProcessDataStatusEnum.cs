using System.ComponentModel;
namespace Susalem.Mam.MamTexProcessDataStatusEnum
{
    /// <summary>
    /// 状态
    /// </summary>
    public enum TexProcessDataStatusEnum
    {
        /// <summary>
        /// 不合格
        /// </summary>
        [Description("不合格")]
        No = 0,

        /// <summary>
        /// 合格
        /// </summary>
        [Description("合格")]
        Ok = 1,

        /// <summary>
        /// 不合格
        /// </summary>
        //[Description("暂定")]
        // new定义为不合格
        [Description("不合格")]
        Failure = 2,
    }
}
