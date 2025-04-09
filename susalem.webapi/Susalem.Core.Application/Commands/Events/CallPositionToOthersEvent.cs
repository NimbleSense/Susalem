using System.Collections.Generic;
using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;

namespace Susalem.Core.Application.Commands.Events
{
    public record CallPositionToOthersEvent(string ConnectionId, bool Registering, ICollection<int> PositionIds) : IRequest<Result>;
}
