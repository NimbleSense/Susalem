namespace Susalem.Core.Application.Models;

/// <summary>
/// 邮件配置属性
/// </summary>
public class MailSetting
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }

    /// <summary>
    /// 服务地址
    /// </summary>
    public string Server { get; set; }

    /// <summary>
    /// 端口
    /// </summary>
    public int Port { get; set; } = 25;

    /// <summary>
    /// 发件人邮箱
    /// </summary>
    public string Sender { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Code { get; set; }
}
