using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;

namespace Susalem.Core.Application.Commands.Events
{
    public record PositionStatusEvent(PositionMonitoringStatus PositionStatus) : IRequest<Result>;
}
