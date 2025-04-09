using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Susalem.Infrastructure.Consumer;

public class EventConsumerHostedService : BackgroundService
{
    private readonly IServiceBus _serviceBus;
    private readonly ILogger<EventConsumerHostedService> _logger;

    public IMonitorEventQueue EventQueue { get; }

    public EventConsumerHostedService(IServiceBus serviceBus,
        IMonitorEventQueue eventQueue,
        ILogger<EventConsumerHostedService> logger)
    {
        _serviceBus = serviceBus;
        EventQueue = eventQueue;
        _logger = logger;
    }

    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem = await EventQueue.DequeueAsync(stoppingToken);
            try
            {
                _logger.LogDebug("Consume notification {@workItem}", workItem);

                var notification = workItem as INotification;
                if(notification != null)
                {
                    await _serviceBus.Publish(notification);
                }

                var request = workItem as IRequest<Result>;
                if (request != null)
                {
                    await _serviceBus.Send(request);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred executing {@WorkItem}.", workItem);
            }
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"Consume notification hosted service is running");
        await BackgroundProcessing(stoppingToken);
    }
}