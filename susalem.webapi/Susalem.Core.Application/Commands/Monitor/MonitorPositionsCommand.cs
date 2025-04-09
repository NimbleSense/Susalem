using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Localize;
using Susalem.Core.Application.Notification;
using Microsoft.Extensions.Localization;

namespace Susalem.Core.Application.Commands.Monitor;

/// <summary>
/// 监控指定点位
/// </summary>
/// <param name="PositionIds"></param>
public record MonitorPositionsCommand(ICollection<int> PositionIds) : IRequest<Result>;

public class RegisterPositionCommandHandler : BaseMessageHandler<MonitorPositionsCommand, Result>
{
    private readonly IPositionFactory _positionFactory;
    private readonly IStringLocalizer<Resource> _localizer;
    private readonly IMonitorEventQueue _eventQueue;

    public RegisterPositionCommandHandler(IPositionFactory positionFactory, 
        IStringLocalizer<Resource> localizer,
        IMonitorEventQueue eventQueue)
    {
        _positionFactory = positionFactory;
        _localizer = localizer;
        _eventQueue = eventQueue;
    }

    public override async Task<Result> HandleAsync(MonitorPositionsCommand request)
    {
        _positionFactory.MonitorPositions(request.PositionIds);
        await _eventQueue.QueueEventItemAsync(new PositionsStatusChanged(true, request.PositionIds));

        var positionNames = new List<string>();
        foreach(var positionId in request.PositionIds)
        {
            var position = _positionFactory.Positions.FirstOrDefault(t => t.Id == positionId);
            positionNames.Add(position.Name);
        }

        return new Result().Successful();
    }
}