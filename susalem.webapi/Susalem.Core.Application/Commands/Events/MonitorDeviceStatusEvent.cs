using System.Collections.Generic;
using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;

namespace Susalem.Core.Application.Commands.Events
{
    public record MonitorDeviceStatusEvent(IReadOnlyCollection<PositionMonitoringStatus> MonitorPositions,
        IReadOnlyCollection<CentralPumpStatus> CentralPumps) : IRequest<Result>;
}
