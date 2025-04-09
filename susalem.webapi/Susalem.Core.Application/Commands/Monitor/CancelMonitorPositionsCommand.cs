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

namespace Susalem.Core.Application.Commands.Monitor
{
    public record CancelMonitorPositionsCommand(ICollection<int> PositionIds) : IRequest<Result>;

    public class UnRegisterPositionCommandHandler : BaseMessageHandler<CancelMonitorPositionsCommand,Result>
    {
        private readonly IPositionFactory _positionFactory;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMonitorEventQueue _eventQueue;

        public UnRegisterPositionCommandHandler(IPositionFactory positionFactory, 
            IStringLocalizer<Resource> localizer,
            IMonitorEventQueue eventQueue)
        {
            _positionFactory = positionFactory;
            _localizer = localizer;
            _eventQueue = eventQueue;
        }

        public override async Task<Result> HandleAsync(CancelMonitorPositionsCommand request)
        {
            _positionFactory.CancelMonitorPositions(request.PositionIds);

            await _eventQueue.QueueEventItemAsync(new PositionsStatusChanged(false, request.PositionIds));

            var positionNames = new List<string>();
            foreach (var positionId in request.PositionIds)
            {
                var position = _positionFactory.Positions.FirstOrDefault(t => t.Id == positionId);
                if(position != null)
                {
                    positionNames.Add(position.Name);
                }
            }

            return new Result().Successful();
        }
    }
}
