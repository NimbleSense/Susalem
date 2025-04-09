using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Position;

namespace Susalem.Core.Application.Commands
{
    public record ConfirmAlarmCommand(ICollection<int> AlarmIds, string ConfirmContent) : IRequest<Result>;

    public class ConfirmAlarmCommandHandler : BaseMessageHandler<ConfirmAlarmCommand, Result>
    {
        private readonly IApplicationAlarmService _alarmService;
        private readonly IPositionFactory _positionFactory;

        public ConfirmAlarmCommandHandler(IApplicationAlarmService alarmService,
            IPositionFactory positionFactory)
        {
            _alarmService = alarmService;
            _positionFactory = positionFactory;
        }

        public override async Task<Result> HandleAsync(ConfirmAlarmCommand request)
        {
            var result = await _alarmService.ConfirmAlarmsAsync(request.AlarmIds, request.ConfirmContent);
            return result; 
        }
    }
}
