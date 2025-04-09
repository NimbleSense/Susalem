namespace Susalem.Shared.Messages.Features.Notification;

/// <summary>
///  发送邮件请求
/// </summary>
/// <param name="Subject">主题</param>
/// <param name="Body">内容</param>
/// <param name="Receiver">收件人</param>
public record SendMailRequest(string Subject, string Body, string Receiver);
