using System.Threading;
using System.Threading.Tasks;
using Susalem.Common.Results;
using Susalem.Messages.Enumerations;

namespace Susalem.Core.Application.Interfaces
{
    public interface IMonitorEventQueue
    {
        Task QueueEventItemAsync(IQueuedEvent eventItem);

        void QueueEventItem(IQueuedEvent eventItem);

        Task<IQueuedEvent> DequeueAsync(CancellationToken token);
    }

    /// <summary>
    /// 排队事件
    /// </summary>
    public interface IQueuedEvent
    {
    }
}
