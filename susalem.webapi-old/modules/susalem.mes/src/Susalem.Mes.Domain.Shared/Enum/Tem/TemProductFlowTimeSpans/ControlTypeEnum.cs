using System.ComponentModel;
namespace Susalem.Tem.TemProductFlowTimeSpans
{
    /// <summary>
    /// 【枚举】 工序时间管控
    /// </summary>
    public enum ControlTypeEnum
    {
        /// <summary>
        /// 时间内提醒
        /// </summary>
        [Description("时间内提醒")]
        RemindWithin = 0,

        /// <summary>
        /// 时间后提醒
        /// </summary>
        [Description("时间后提醒")]
        RemindAfter = 1,

        /// <summary>
        /// 时间到提醒
        /// </summary>
        [Description("时间到提醒")]
        RemindWhen = 2
    }
}
