namespace Susalem.Core.Application.ReadModels.Statistics
{
    public class BasicInfoQueryModel
    {
        /// <summary>
        /// 设备数量
        /// </summary>
        public int DeviceCount { get; set; }

        /// <summary>
        /// 区域数量
        /// </summary>
        public int AreaCount { get; set; }

        /// <summary>
        /// 点位数量
        /// </summary>
        public int PositionCount { get; set; }

        /// <summary>
        /// 未处理的报警数量
        /// </summary>
        public int UnHandledAlarmCount { get; set; }
    }
}
