namespace Susalem.Messages.Enumerations;
/// <summary>
/// 通知类型
/// </summary>
public enum NotificationEnum
{
    /// <summary>
    /// 无固定类型
    /// </summary>
    None,
    /// <summary>
    /// 邮件
    /// </summary>
    Mail,
    Webhook,
}

/// <summary>
/// 通知事件类型
/// </summary>
public enum NotificationEventEnum
{
    /// <summary>
    /// 点位记录
    /// </summary>
    Record,
    /// <summary>
    ///  告警
    /// </summary>
    Alarm
}
