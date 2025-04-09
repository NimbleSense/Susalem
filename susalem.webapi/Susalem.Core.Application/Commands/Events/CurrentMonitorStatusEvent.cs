using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;

namespace Susalem.Core.Application.Commands.Events
{
    /// <summary>
    /// 客户端第一次连接，获取现有的监控连接状态事件
    /// </summary>
    public record CurrentMonitorStatusEvent(string ConnectionId) : IRequest<Result>;
}
