using System.ComponentModel;
namespace Susalem.Mam.MesProcessStatusEnum
{
    /// <summary>
    /// 返修管理加工状态
    /// </summary>
    public enum ProcessStatusEnum
    {
        /// <summary>
        /// 合格
        /// </summary>
        [Description("合格")]
        OK = 1,

        /// <summary>
        /// 不合格
        /// </summary>
        [Description("不合格")]
        NG = 2,

        /// <summary>
        /// 报废
        /// </summary>
        [Description("报废")]
        Scrap = 3
    }
}
