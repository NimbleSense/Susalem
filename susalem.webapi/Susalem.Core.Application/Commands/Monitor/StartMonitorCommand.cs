using System.Collections.Generic;
using Susalem.Common.Extensions;
using System.Threading.Tasks;
using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Susalem.Core.Application.Localize;
using Microsoft.Extensions.Localization;
using Susalem.Core.Application.Notification;

namespace Susalem.Core.Application.Commands.Monitor;

/// <summary>
/// 开启监控，并选择指定点位
/// </summary>
/// <param name="PositionIds"></param>
public record StartMonitorCommand(ICollection<int> PositionIds) : IRequest<Result>;

/// <summary>
/// 处理开启监控命令
/// </summary>
public class StartMonitorCommandHandler : BaseMessageHandler<StartMonitorCommand, Result>
{
    private readonly ILogger<StartMonitorCommandHandler> _logger;
    private readonly IMonitorLoop _monitorLoop;
    private readonly IStringLocalizer<Resource> _localizer;
    private readonly IPositionFactory _positionFactory;
    private readonly IMonitorEventQueue _eventQueue;
    private readonly IServiceBus _serviceBus;

    public StartMonitorCommandHandler(ILogger<StartMonitorCommandHandler> logger,
        IPositionFactory positionFactory,
        IMonitorLoop monitorLoop, 
        IStringLocalizer<Resource> localizer,
        IMonitorEventQueue eventQueue)
    {
        _logger = logger;
        _monitorLoop = monitorLoop;
        _localizer = localizer;
        _positionFactory = positionFactory;
        _eventQueue = eventQueue;
    }

    public override async Task<Result> HandleAsync(StartMonitorCommand request)
    {
        _logger.LogInformation("Start monitor");
        _positionFactory.MonitorPositions(request.PositionIds);
        _monitorLoop.StartMonitorLoop();

        await _eventQueue.QueueEventItemAsync(new MonitoringStatusChanged(true));
        await _eventQueue.QueueEventItemAsync(new PositionsStatusChanged(true, request.PositionIds));

        return new Result().Successful();
    }
}
