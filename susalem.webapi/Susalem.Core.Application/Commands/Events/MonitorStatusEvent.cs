using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;

namespace Susalem.Core.Application.Commands.Events
{
    /// <summary>
    /// 客户端第一次连接，更新现有状态
    /// </summary>
    public record MonitorStatusEvent : IRequest<Result>;
}
