using System.Collections.Generic;

namespace Susalem.Core.Application.Models;

/// <summary>
/// Webhook 配置
/// </summary>
public class WebhookSetting
{
    public bool IsEnable { get; set; } = false;

    /// <summary>
    /// URL地址
    /// </summary>
    public string WebHookUrl { get; set; }

    /// <summary>
    /// 是否上传数据记录
    /// </summary>
    public bool PostRecord { get; set; }

    /// <summary>
    /// 是否上传告警记录
    /// </summary>
    public bool PostAlarm { get; set; }
}
