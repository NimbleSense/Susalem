using Susalem.Core.Application.Interfaces;
using Susalem.Infrastructure.Consumer;
using Microsoft.Extensions.DependencyInjection;

namespace Susalem.Infrastructure.SignalR;

public static class ServiceCollectionExtension
{
    public static void AddConsumer(this IServiceCollection services)
    {
        services.AddSingleton<IMonitorEventQueue,MonitorEventQueue>();
        services.AddHostedService<EventConsumerHostedService>();
    }
}
