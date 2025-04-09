using System.Threading.Tasks;
using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application;
using Susalem.Core.Application.Commands.Events;
using Microsoft.AspNetCore.SignalR;

namespace Susalem.Api.Handlers;

/// <summary>
/// 点位状态更新状态，通过Signalr发送到前端
/// </summary>
public class PositionStatusEventHandler : BaseMessageHandler<PositionStatusEvent, Result>
{
    private readonly IHubContext<MonitorHub, IMessageNotification> _hubContext;

    public PositionStatusEventHandler(IHubContext<MonitorHub, IMessageNotification> hubContext)
    {
        _hubContext = hubContext;
    }

    public override async Task<Result> HandleAsync(PositionStatusEvent request)
    {
        await _hubContext.Clients.All.PositionStatusChanged(request.PositionStatus);
        return new Result().Successful();
    }
}