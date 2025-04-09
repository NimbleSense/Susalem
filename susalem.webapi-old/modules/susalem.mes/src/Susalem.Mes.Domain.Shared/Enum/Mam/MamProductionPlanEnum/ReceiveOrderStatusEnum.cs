using System.ComponentModel;
namespace Susalem.Mam.MamProductionPlanEnum
{
    /// <summary>
    /// 外部接收订单表
    /// </summary>
    public enum ReceiveOrderStatusEnum
    {
        /// <summary>
        /// 排队中
        /// </summary>
        [Description("排队中")]
        Queuing =0,

        /// <summary>
        /// 运行
        /// </summary>
        [Description("运行")]
        Working =1,

        /// <summary>
        /// 已关闭
        /// </summary>
        [Description("已关闭")]
        Close=2,
 


    }
}
