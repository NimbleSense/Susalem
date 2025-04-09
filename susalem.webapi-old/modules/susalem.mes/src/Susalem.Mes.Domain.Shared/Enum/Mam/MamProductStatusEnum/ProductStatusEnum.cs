using System.ComponentModel;
namespace Susalem.Mam.MamProductStatusEnum
{
    /// <summary>
    /// 产品状态枚举
    /// </summary>
    public enum ProductStatusEnum
    {
        /// <summary>
        /// 加工中
        /// </summary>
        [Description("加工中")]
        Processing = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Complete = 5,

        /// <summary>
        /// 合格
        /// </summary>
        [Description("合格")]
        Ok = 6,

        /// <summary>
        /// 不合格
        /// </summary>
        [Description("不合格")]
        Ng = 7,

        /// <summary>
        /// 报废
        /// </summary>
        [Description("报废")]
        Scrap = 8,

        /// <summary>
        /// 使用
        /// </summary>
        [Description("使用")]
        Use = 9,

        /// <summary>
        /// 解绑
        /// </summary>
        [Description("解绑")]
        Unlink = 10,

        /// <summary>
        /// 异常
        /// </summary>
        [Description("异常")]
        Error = 11,
    }
}
