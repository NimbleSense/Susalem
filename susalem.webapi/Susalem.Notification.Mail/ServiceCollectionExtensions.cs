using Susalem.Core.Application.Interfaces.Providers;
using Susalem.Core.Application.Models;
using Susalem.Notification.Cloud.Handlers;
using Susalem.Notification.Mail.Provider;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.DependencyInjection;

namespace Susalem.Notification.Mail;

public static class ServiceCollectionExtensions
{
    public static void AddMail(this IServiceCollection services)
    {
        services.AddScoped<MailSetting>();
        services.AddSingleton<IMailProvider, MailProvider>();
    }

    public static void AddMailConsumer(this IServiceCollectionMediatorConfigurator mediator)
    {
        mediator.AddConsumer<AlarmEventGeneratedNotificationHandler>();
    }
}
