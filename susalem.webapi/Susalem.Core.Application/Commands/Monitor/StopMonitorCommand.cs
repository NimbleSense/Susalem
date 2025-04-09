using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Localize;
using Susalem.Core.Application.Notification;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Susalem.Core.Application.Commands.Monitor;

/// <summary>
/// 停止监控
/// </summary>
public record StopMonitorCommand : IRequest<Result>;

public class StopMonitorCommandHandler : BaseMessageHandler<StopMonitorCommand, Result>
{
    private readonly ILogger<StopMonitorCommandHandler> _logger;
    private readonly IMonitorLoop _monitorLoop;
    private readonly IPositionFactory _positionFactory;
    private readonly IAlarmValidator _alarmValidator;
    private readonly IStringLocalizer<Resource> _localizer;
    private readonly IMonitorEventQueue _eventQueue;

    public StopMonitorCommandHandler(ILogger<StopMonitorCommandHandler> logger,
        IMonitorLoop monitorLoop,
        IPositionFactory positionFactory,
        IAlarmValidator alarmValidator,
        IStringLocalizer<Resource> localizer,
        IMonitorEventQueue eventQueue)
    {
        _logger = logger;
        _monitorLoop = monitorLoop;
        _positionFactory = positionFactory;
        _alarmValidator = alarmValidator;
        _localizer = localizer;
        _eventQueue = eventQueue;
    }

    public override async Task<Result> HandleAsync(StopMonitorCommand request)
    {
        _monitorLoop.StopMonitorLoop();
        _positionFactory.CancelMonitorPositions(_positionFactory.MonitoringPositions.Keys);

        await _eventQueue.QueueEventItemAsync(new MonitoringStatusChanged(false));
        await _eventQueue.QueueEventItemAsync(new PositionsStatusChanged(false, _positionFactory.MonitoringPositions.Keys));

        _alarmValidator.ClearCachedAlarms();
        return new Result().Successful();
    }
}