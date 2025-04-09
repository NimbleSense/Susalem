using Susalem.Core.Application;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Repositories;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Models;
using Susalem.Infrastructure.DbContext;
using Susalem.Infrastructure.Device;
using Susalem.Infrastructure.Mappings;
using Susalem.Infrastructure.Mappings.Resolvers;
using Susalem.Infrastructure.Models.Identity;
using Susalem.Infrastructure.Repositories;
using Susalem.Infrastructure.Services;
using Susalem.Infrastructure.Services.DbInitializerService;
using Susalem.Infrastructure.SignalR;
using Susalem.Notification.Cloud;
using Susalem.Notification.Mail;
using Susalem.Persistence.DbContext;
using Susalem.Persistence.Npgsql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Susalem.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddIdentity();

        services.AddScoped<DeviceResolver>();
        services.AddScoped<DeviceFunctionValueUnitResolver>();
        services.AddAutoMapper(config =>
        {
            config.AddProfile<AppProfile>();
            config.AddProfile<UserProfile>();
            config.AddProfile<RecordProfile>();
        });

        services.AddSingleton<IServiceBus, ServiceBusMediator>();

        services.AddDeviceMonitor(configuration);

        services.AddConsumer();

        //Notification
        services.AddMail();
        services.AddWebhook();
    }

    public static void AddSharedService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IJwtFactory, JwtFactory>();

        services.AddScoped<IChannelService, ChannelService>();
        services.AddScoped<IApplicationConfigurationService, ApplicationConfigurationService>();
        services.AddScoped<IApplicationDeviceService, ApplicationDeviceService>();
        services.AddScoped<IApplicationPositionService, ApplicationPositionService>();
        services.AddScoped<IApplicationAlarmService, ApplicationAlarmService>();
        services.AddScoped<IRecordService, Services.RecordService>();
        services.AddScoped<IAlarmRuleService, AlarmRuleService>();
        services.AddScoped<IStatisticsService, StatisticsService>();

        services.AddSingleton<IAlarmValidator, AlarmValidator>();
    }

    public static void AddDatabasePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<TenantInfo>();

        services.AddDbContext<IdentityDbContext>();
        services.AddDbContext<ApplicationDbContext>();

        var dbType = configuration["Db:Provider"].ToString();
        if (dbType.ToLower().Equals("npgsql"))
        {
            services.AddDbContext<RecordDbContext, NpgSqlRecordDbContext>();
        }
        else
        {
            services.AddDbContext<RecordDbContext>();
        }
        services.AddScoped<IDbInitializerService, DbInitializerService>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IEntityRepository<,>),typeof(DataEntityRepository<,>));
    }

    private static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<IdentityDbContext>();

        services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
        services.AddScoped<IApplicationUserService, ApplicationUserService>();
    }


    public static void UseInfrastructureLayer(this IApplicationBuilder builder)
    {
    }
}
