using System.Threading.Tasks;
using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application;
using Susalem.Core.Application.Commands.Events;
using Microsoft.AspNetCore.SignalR;

namespace Susalem.Api.Handlers;

/// <summary>
/// 设备在线状态变化，通过Signalr发送到前端
/// </summary>
public class DevicesStatusChangedEventHandler : BaseNotificationMessageHandler<DevicesStatusChangedEvent>
{
    private readonly IHubContext<MonitorHub, IMessageNotification> _hubContext;

    public DevicesStatusChangedEventHandler(IHubContext<MonitorHub, IMessageNotification> hubContext)
    {
        _hubContext = hubContext;
    }

    public override async Task<Result> HandleAsync(DevicesStatusChangedEvent request)
    {
        await _hubContext.Clients.All.DevicesStatusChanged(request.DeviceIds, request.Status);
        return new Result().Successful();
    }
}