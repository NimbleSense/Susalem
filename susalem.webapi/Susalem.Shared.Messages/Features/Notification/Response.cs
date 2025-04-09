using Susalem.Messages.Enumerations;

namespace Susalem.Shared.Messages.Features.Notice;

/// <summary>
/// 失败的通知信息
/// </summary>
/// <param name="Id"></param>
/// <param name="Content"></param>
/// <param name="ContentType"></param>
/// <param name="Exception"></param>
/// <param name="SendTime"></param>
/// <param name="NoticeType"></param>
/// <param name="NoticeEvent"></param>
/// <param name="Ignored"></param>
public record FailedNoticeResponse(int Id, 
    string Content, 
    string ContentType, 
    string Exception,
    DateTime SendTime,
    NotificationEnum NoticeType, 
    NotificationEventEnum NoticeEvent,
    bool Ignored);

public record NoticeInfoResponse(string SmsInfo, string MailInfo, string WebhookInfo, string CloudInfo);