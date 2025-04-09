using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;

namespace Susalem.Core.Application.Commands.Events
{
    public record CallAlerterStatusToOthersEvent(string ConnectionId, bool IsEnabledAlerter) : IRequest<Result>;
}
