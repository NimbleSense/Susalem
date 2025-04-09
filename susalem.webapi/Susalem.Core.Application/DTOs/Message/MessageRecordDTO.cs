using System;

namespace Susalem.Core.Application.DTOs.Message;

public class MessageRecordDTO
{
    /// <summary>
    /// 目标用途
    /// </summary>
    public string Target { get; set; }

    /// <summary>
    /// 是否发送成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime SendTime { get; set; }

    /// <summary>
    /// 消息通知外键
    /// </summary>
    public int MessageSettingId { get; set; }

    /// <summary>
    /// 报警外键
    /// </summary>
    public int AlarmId { get; set; }

    /// <summary>
    /// 报警规则
    /// </summary>
    public int AlarmRuleId { get; set; }
}
