using Susalem.Core.Application.Commands;
using Susalem.Core.Application.Commands.Auth;
using Susalem.Core.Application.Commands.Config;
using Susalem.Core.Application.Commands.Events;
using Susalem.Core.Application.Commands.Monitor;
using Susalem.Core.Application.Commands.Users;
using Susalem.Core.Application.Queries.Config;
using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace Susalem.Core.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationConsumer(this IServiceCollectionMediatorConfigurator mediator)
    {
        mediator.AddConsumer<AuthCommandHandler>();
        mediator.AddConsumer<UnAuthCommandHandler>();

        mediator.AddUserConsumer();
        mediator.AddAppConfigConsumer();
        mediator.AddMonitorConsumer();
    }

    private static void AddUserConsumer(this IServiceCollectionMediatorConfigurator mediator)
    {
        mediator.AddConsumer<ChangeUserPasswordCommandHandler>();
        mediator.AddConsumer<CreateUserCommandHandler>();
    }

    private static void AddAppConfigConsumer(this IServiceCollectionMediatorConfigurator mediator)
    {
        mediator.AddConsumer<UpdateApplicationConfigurationHandler>();
        mediator.AddConsumer<GetConfigQueryHandler>();
    }

    private static void AddMonitorConsumer(this IServiceCollectionMediatorConfigurator mediator)
    {
        mediator.AddConsumer<RegisterPositionCommandHandler>();
        mediator.AddConsumer<UnRegisterPositionCommandHandler>();
        mediator.AddConsumer<StartMonitorCommandHandler>();
        mediator.AddConsumer<StopMonitorCommandHandler>();
        mediator.AddConsumer<ConfirmAlarmCommandHandler>();
        mediator.AddConsumer<SetAlerterBuzzingCommandHandler>();
        mediator.AddConsumer<SetAlerterLightingCommandHandler>();

        mediator.AddConsumer<SavePositionRecordsHandler>();
        mediator.AddConsumer<RecordsAlarmValidationHandler>();
        mediator.AddConsumer<AlarmToEventQueueHandler>();
        mediator.AddConsumer<PositionRecordToQueueHandler>();
    }
}