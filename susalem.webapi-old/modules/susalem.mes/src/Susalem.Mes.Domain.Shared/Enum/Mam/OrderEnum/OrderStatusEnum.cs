using System.ComponentModel;
namespace Susalem.Mam.OrderEnum
{
    /// <summary>
    /// 订单状态枚举
    /// </summary>
    public enum OrderStatusEnum
    {
        /// <summary>
        /// 新建
        /// </summary>
        [Description("新建")]
        New = 0,

        /// <summary>
        /// 下达
        /// </summary>
        [Description("下达")]
        Release = 1,

        /// <summary>
        /// 加工中
        /// </summary>
        [Description("加工中")]
        Processing = 2,

        /// <summary>
        /// 暂停
        /// </summary>
        [Description("暂停")]
        Stop = 3,

        /// <summary>
        /// 已关闭
        /// </summary>
        [Description("已关闭")]
        Close = 4,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Complete = 5,
    }
}
