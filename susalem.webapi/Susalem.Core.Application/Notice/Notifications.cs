using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.ReadModels.Alarm;
using Susalem.Messages.Enumerations;
using System.Collections.Generic;

namespace Susalem.Core.Application.Notification;

/// <summary>
/// 支持重试的通知,可排队事件
/// </summary>
public abstract record RetryNotification : IQueuedEvent
{
    /// <summary>
    /// 失败存储表的Id
    /// </summary>
    public int FailedId { get; set; } = -1;

    /// <summary>
    /// 消息类型
    /// </summary>
    public NotificationEnum NoticeType { get; set; } = NotificationEnum.None;

    /// <summary>
    /// 是否是重试操作
    /// </summary>
    public bool IsRetry
    {
        get => FailedId > 0;
    }

    /// <summary>
    /// 重试是否成功
    /// </summary>
    public bool RetrySuccess { get; set; }
}

/// <summary>
/// 点位记录数据产生广播事件
/// </summary>
/// <param name="Records">点位记录</param>
/// <param name="Id">失败记录存储Id, -1表示是实时数据</param>
public record PositionRecordNotification(IList<RecordRequestDTO> Records) : RetryNotification, INotification;

/// <summary>
/// 系统监控状态变化
/// </summary>
/// <param name="Monitoring">是否在监控中</param>
public record MonitoringStatusChanged(bool Monitoring) : INotification, IQueuedEvent;

/// <summary>
/// 点位指定监控状态变化
/// </summary>
/// <param name="Monitoring">是否在监控中</param>
/// <param name="PositionIds">影响的点位ID列表</param>
public record PositionsStatusChanged(bool Monitoring, ICollection<int> PositionIds) : INotification, IQueuedEvent;

/// <summary>
/// 告警事件通知
/// </summary>
/// <param name="AlarmEvent">告警内容</param>
/// <param name="AlarmRule">告警规则</param>
/// <param name="Id">失败记录存储Id, -1表示是实时数据</param>
public record AlarmEventGenerated(AlarmQueryModel AlarmContent, AlarmRuleQueryModel AlarmRule) : RetryNotification, INotification;
