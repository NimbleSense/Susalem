using System.Threading.Tasks;
using Susalem.Core.Application;
using Susalem.Core.Application.Commands.Events;
using Microsoft.AspNetCore.SignalR;

namespace Susalem.Api.Handlers;

/// <summary>
/// 报警通知，通过SignalR发送到前端
/// </summary>
public class AlarmNotificationHandler : BaseNotificationMessageHandler<AlarmGenerated>
{
    private readonly IHubContext<MonitorHub, IMessageNotification> _hubContext;

    public AlarmNotificationHandler(IHubContext<MonitorHub, IMessageNotification> hubContext)
    {
        _hubContext = hubContext;
    }

    public override async Task HandleAsync(AlarmGenerated request)
    {
        await _hubContext.Clients.All.AlarmReported(request.Alarm);
    }
}