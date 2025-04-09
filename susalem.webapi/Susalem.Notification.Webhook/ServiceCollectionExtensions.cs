
using Susalem.Core.Application;
using Susalem.Core.Application.Interfaces.Providers;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Models;
using Susalem.Notification.Webhook.Handlers;
using Susalem.Notification.Webhook.Provider;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.DependencyInjection;

namespace Susalem.Notification.Cloud;

public static class ServiceCollectionExtensions
{
    public static void AddWebhook(this IServiceCollection services)
    {
        services.AddScoped<WebhookSetting>(sp =>
        {
            var configurationService = sp.GetService<IApplicationConfigurationService>();
            return configurationService.GetValue<WebhookSetting>(Configuration.WebhookSettingKey);
        });
        services.AddScoped<IWebhookProvider, WebhookProvider>();
        services.AddHttpClient();
    }

    public static void AddwebhookConsumer(this IServiceCollectionMediatorConfigurator mediator)
    {
        mediator.AddConsumer<PositionRecordNotificationHandler>();
        mediator.AddConsumer<AlarmEventGeneratedNotificationHandler>();
    }
}
