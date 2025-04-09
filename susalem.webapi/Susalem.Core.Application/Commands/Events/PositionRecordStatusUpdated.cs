using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Notification;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Commands.Events
{
    /// <summary>
    /// 点位记录的设备状态已更新(报警验证成功后，更新点位记录中的设备状态)
    /// </summary>
    /// <param name="PositionId">点位ID</param>
    /// <param name="Records">点位记录</param>
    public record PositionRecordStatusUpdated(int PositionId, IList<RecordRequestDTO> Records, bool Door1, bool Door2, bool Door3, bool Door4) : INotification;

    /// <summary>
    /// 点位记录的设备状态通知到消息通知事件队列
    /// </summary>
    public class PositionRecordToQueueHandler : BaseNotificationMessageHandler<PositionRecordStatusUpdated>
    {
        private readonly IMonitorEventQueue _eventQueue;

        public PositionRecordToQueueHandler(IMonitorEventQueue eventQueue)
        {
            _eventQueue = eventQueue;
        }

        public override async Task HandleAsync(PositionRecordStatusUpdated request)
        {
            await _eventQueue.QueueEventItemAsync(new PositionRecordNotification(request.Records));
        }
    }
}
