using Susalem.Core.Application.Notification;
using Susalem.Core.Application;
using Susalem.Core.Application.Models;
using Susalem.Messages.Enumerations;
using Susalem.Core.Application.Interfaces.Providers;

namespace Susalem.Notification.Webhook.Handlers;

/// <summary>
/// 点位记录通知处理器
/// </summary>
public class PositionRecordNotificationHandler : BaseNotificationMessageHandler<PositionRecordNotification>
{
    private readonly WebhookSetting _webhookSetting;
    private readonly IWebhookProvider _webhookProvider;

    public PositionRecordNotificationHandler( 
        WebhookSetting webhookSetting, 
        IWebhookProvider webhookProvider)
    {
        _webhookSetting = webhookSetting;
        _webhookProvider = webhookProvider;
    }


    public async override Task HandleAsync(PositionRecordNotification request)
    {
        if (!_webhookSetting.IsEnable) return;

        if (!_webhookSetting.PostRecord) return;

        //如果是重试操作，需要确定是否是当前类型的重试
        if (request.IsRetry && request.NoticeType != NotificationEnum.Webhook) return;

        var result = await _webhookProvider.PostDataAsync(request.Records, _webhookSetting.WebHookUrl, "/data");

        if (result.Succeeded)
        {
            if (request.IsRetry)
            {
                //如果是重试操作，则删除记录
                //await _noticeService.DeleteAsync(request.FailedId);
                request.RetrySuccess = true;
            }
        }
        else
        {
            if (!request.IsRetry)
            {
                //新操作纪录，如果失败则需要保存记录
                //await _noticeService.CreateNoticeAsync(NotificationEnum.Webhook, NotificationEventEnum.Record, request, result.MessageWithErrors);
            }
        }
    }
}
