using Susalem.Core.Application;
using Susalem.Core.Application.Interfaces.Providers;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Localize;
using Susalem.Core.Application.Notification;
using Susalem.Messages.Enumerations;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Susalem.Notification.Cloud.Handlers;

/// <summary>
/// 告警事件通知处理
/// </summary>
public class AlarmEventGeneratedNotificationHandler : BaseNotificationMessageHandler<AlarmEventGenerated>
{
    public const string MailTemplate = @"
<html>

<head>
    <style>
        table {
            border-collapse: collapse;
        }
        th {
            border: 1px solid gray;
            padding: 15px;
            font-size: 15px;
            text-align: left;
            background-color: cornflowerblue;
            color: white
        }
        td {
            border: 1px solid gray;
            padding: 5px;
            font-size: 15px;
            text-align: left;
        }
    </style>
</head>

<body>
    <br/>
<table>
    <tr>
        <th>功能</th>
        <th>告警值</th>
        <th>发生时间</th>
    </tr>
    {MailContent}
</table>
    <h3 style=""color:red"">请及时检查!
</body>
</html>
";
    private readonly ILogger<AlarmEventGeneratedNotificationHandler> _logger;
    private readonly IApplicationUserService _userService;
    private readonly IMailProvider _mailProvider;
    private readonly IStringLocalizer<Resource> _localizer;

    public AlarmEventGeneratedNotificationHandler(
        ILogger<AlarmEventGeneratedNotificationHandler> logger,
        IApplicationUserService userService,
        IMailProvider mailProvider,
        IStringLocalizer<Resource> localizer)
    {
        _logger = logger;
        _userService = userService;
        _mailProvider = mailProvider;
        _localizer = localizer;
    }

    public async override Task HandleAsync(AlarmEventGenerated request)
    {
        if (!_mailProvider.Setting.IsEnable) return;

        //如果是重试操作，需要确定是否是当前类型的重试
        if (request.IsRetry && request.NoticeType != NotificationEnum.Mail) return;

        string errorMsg;
        try
        {
            var usersResult = await _userService.GetAllUsersAsync();
            if (usersResult.Failed) return;

            var alarmContent = new StringBuilder();
            foreach (var alarmDetail in request.AlarmContent.AlarmDetails)
            {
                alarmContent.Append($"<tr><td>{_localizer[alarmDetail.AlarmProperty]}</td><td>{alarmDetail.AlarmValue}</td><td>{alarmDetail.ReportTime}</td></tr>");
            }

            var html = MailTemplate.Replace("{MailContent}", alarmContent.ToString());
            var users = usersResult.Data.Where(t => request.AlarmRule.Notification.Contacts.Contains(t.Id.ToString()));

            var result = await _mailProvider.SendAsync($"{request.AlarmContent.PositionName} {_localizer[request.AlarmRule.AlarmLevel.ToString()]}",
                html, users.Select(t => t.Email).ToList());

            if (result.Succeeded)
            {
                
                return;
            }
            errorMsg = result.MessageWithErrors;
        }
        catch (Exception ex)
        {
            _logger.LogError("Handle mail alarm exception: {ex}", ex);
            errorMsg = ex.Message;
        }
    }
}
