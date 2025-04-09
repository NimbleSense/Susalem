using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;
using System;
using System.Threading.Channels;
using System.Threading;
using System.Threading.Tasks;

namespace Susalem.Infrastructure.Consumer;

internal class MonitorEventQueue:IMonitorEventQueue
{
    private readonly Channel<IQueuedEvent> _queue;

    public MonitorEventQueue()
    {
        var options = new BoundedChannelOptions(200)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        _queue = Channel.CreateBounded<IQueuedEvent>(options);
    }

    public async Task QueueEventItemAsync(IQueuedEvent eventItem)
    {
        if (eventItem == null)
        {
            throw new ArgumentNullException(nameof(eventItem));
        }

        await _queue.Writer.WriteAsync(eventItem);
    }

    public void QueueEventItem(IQueuedEvent eventItem)
    {
        if (eventItem == null)
        {
            throw new ArgumentNullException(nameof(eventItem));
        }

        _queue.Writer.TryWrite(eventItem);
    }

    public async Task<IQueuedEvent> DequeueAsync(CancellationToken token)
    {
        var workItem = await _queue.Reader.ReadAsync(token);
        return workItem;
    }
}
