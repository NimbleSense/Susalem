using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Notification;
using Susalem.Core.Application.ReadModels.Alarm;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Commands.Events;

/// <summary>
/// 报警产生事件
/// </summary>
/// <param name="AlarmRule">报警规则</param>
/// <param name="Alarm">报警内容</param>
public record AlarmGenerated(AlarmRuleQueryModel AlarmRule, AlarmQueryModel Alarm) : INotification;

/// <summary>
/// 将报警事件发送到通知队列
/// </summary>
public class AlarmToEventQueueHandler : BaseNotificationMessageHandler<AlarmGenerated>
{
    private readonly IMonitorEventQueue _eventQueue;

    public AlarmToEventQueueHandler(IMonitorEventQueue eventQueue)
    {
        _eventQueue = eventQueue;
    }

    public override async Task HandleAsync(AlarmGenerated request)
    {
        await _eventQueue.QueueEventItemAsync(new AlarmEventGenerated(request.Alarm, request.AlarmRule));
    }
}