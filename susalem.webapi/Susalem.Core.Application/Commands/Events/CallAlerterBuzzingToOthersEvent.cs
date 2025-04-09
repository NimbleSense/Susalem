using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;

namespace Susalem.Core.Application.Commands.Events
{
    public record CallAlerterBuzzingToOthersEvent(string ConnectionId, bool IsEnableBuzzing) : IRequest<Result>;
}
