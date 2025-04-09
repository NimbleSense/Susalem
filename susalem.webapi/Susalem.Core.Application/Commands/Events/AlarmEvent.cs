using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.ReadModels.Alarm;

namespace Susalem.Core.Application.Commands.Events
{
    public record AlarmEvent(AlarmQueryModel QueryModel) : IRequest<Result>;
}
