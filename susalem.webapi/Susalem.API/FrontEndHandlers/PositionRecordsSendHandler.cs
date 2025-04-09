using System.Threading.Tasks;
using Susalem.Core.Application;
using Susalem.Core.Application.Commands.Events;
using Microsoft.AspNetCore.SignalR;

namespace Susalem.Api.Handlers
{
    /// <summary>
    /// 点位数据生成成功并且包含告警状态，通过Signalr通知前端
    /// </summary>
    public class PositionRecordsSendHandler:BaseNotificationMessageHandler<PositionRecordStatusUpdated>
    {
        private readonly IHubContext<MonitorHub, IMessageNotification> _hubContext;

        public PositionRecordsSendHandler(IHubContext<MonitorHub, IMessageNotification> hubContext)
        {
            _hubContext = hubContext;
        }

        public override async Task HandleAsync(PositionRecordStatusUpdated request)
        {
            foreach (var record in request.Records)
            {
                await _hubContext.Clients.All.PositionRecordReported(request.PositionId,
                    record.PositionFunction.ToString(), 
                    record.Contents, 
                    request.Door1, 
                    request.Door2, 
                    request.Door3, 
                    request.Door4);
            }
        }
    }
}
