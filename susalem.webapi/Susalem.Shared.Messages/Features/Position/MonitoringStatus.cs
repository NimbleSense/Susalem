using System.Collections.Generic;

namespace Susalem.Core.Application.DTOs
{
    /// <summary>
    /// 点位监控状态
    /// </summary>
    public class PositionMonitoringStatus
    {
        public int Id { get; set; }

        public IList<DeviceDetailStatus> DeviceDetails { get; set; }
    }

    /// <summary>
    /// 设备详细状态
    /// </summary>
    public class DeviceDetailStatus
    {
        public int Id { get; set; }

        public DeviceStatus DeviceStatus { get; set; }
    }

    public enum PositionStatus
    {
        /// <summary>
        /// 点位正常
        /// </summary>
        PositionNormal,
        /// <summary>
        /// 点位预警
        /// </summary>
        PositionWarning,
        /// <summary>
        /// 点位报警
        /// </summary>
        PositionAlarm
    }

    public enum DeviceStatus
    {
        /// <summary>
        /// 设备未启动
        /// </summary>
        NotStarted,
        /// <summary>
        /// 设备离线
        /// </summary>
        Offline,
        /// <summary>
        /// 设备正常
        /// </summary>
        Normal,
        /// <summary>
        /// 设备预警
        /// </summary>
        Warning,
        /// <summary>
        /// 设备告警
        /// </summary>
        Alarm
    }

    ///// <summary>
    ///// 报警器状态
    ///// </summary>
    //public class AlerterStatus
    //{
    //    public int Id { get; set; }

    //    /// <summary>
    //    /// 是否允许发出蜂鸣声 （显示黄色和红色并且运行蜂鸣，则代表正在蜂鸣）
    //    /// </summary>
    //    public bool IsEnableBuzzing { get; set; }

    //    /// <summary>
    //    /// 报警器是否故障
    //    /// </summary>
    //    public bool IsFault { get; set; }

    //    /// <summary>
    //    /// 当前正在显示的灯光
    //    /// </summary>
    //    public string Lighting { get; set; }
    //}

    /// <summary>
    /// 中央泵状态
    /// </summary>
    public class CentralPumpStatus
    {
        public int Id { get; set; }

        /// <summary>
        /// 中央泵是否故障
        /// </summary>
        public bool IsFault { get; set; }

        /// <summary>
        /// 中央泵是否在运行中
        /// </summary>
        public bool IsRunning { get; set; }
    }
}
