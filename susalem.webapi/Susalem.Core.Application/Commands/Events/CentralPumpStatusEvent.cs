using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;

namespace Susalem.Core.Application.Commands.Events
{
    /// <summary>
    /// 中央泵状态事件
    /// </summary>
    public record CentralPumpStatusEvent() : IRequest<Result>;
}
