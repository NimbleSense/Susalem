using System.ComponentModel;

namespace Susalem.Mam.MamProductionPlanEnum
{
    /// <summary>
    /// 是否下达区分枚举
    /// </summary>
    public enum ConversionModeEnum
    {
        /// <summary>
        /// 不下达
        /// </summary>
        [Description("不下达")]
        NotRun = 0,

        /// <summary>
        /// 确认下达下达
        /// </summary>
        [Description("确认下达")]
        IsRun = 1,

        /// <summary>
        /// 选择
        /// </summary>
        [Description("选择下达")]
        ChooseRun = 2
    }
}
