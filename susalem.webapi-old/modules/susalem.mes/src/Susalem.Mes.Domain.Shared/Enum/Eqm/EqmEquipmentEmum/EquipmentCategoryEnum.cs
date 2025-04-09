using System.ComponentModel;

namespace Susalem.Eqm.EqmEquipmentEmum
{
    /// <summary>
    /// 设备类型枚举
    /// </summary>
    public enum EquipmentCategoryEnum
    {
        /// <summary>
        /// 自动工位
        /// </summary>
        [Description("自动工位")]
        A = 0,

        /// <summary>
        /// 半自动工位
        /// </summary>
        [Description("半自动工位")]
        SA = 1,

        /// <summary>
        /// 手动工位
        /// </summary>
        [Description("手动工位")]
        M = 2,

        /// <summary>
        /// 预留工位
        /// </summary>
        [Description("预留工位")]
        R = 3,

        /// <summary>
        /// 返修岔道
        /// </summary>
        [Description("返修岔道")]
        RT = 4,

        /// <summary>
        /// 键槽
        /// </summary>
        [Description("键槽")]
        S = 5,

        /// <summary>
        /// 抽检工位
        /// </summary>
        [Description("抽检工位")]
        SC = 6
    }
}
