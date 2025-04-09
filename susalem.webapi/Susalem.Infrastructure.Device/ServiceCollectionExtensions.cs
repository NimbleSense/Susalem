using System;
using Susalem.Core.Application;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Susalem.Infrastructure.Device
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeviceMonitor(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddSingleton(sp =>
            {
                using var scope = sp.CreateScope();
                var configurationService = scope.ServiceProvider.GetService<IApplicationConfigurationService>();
                return configurationService.GetValue<MonitorSetting>(Configuration.MonitorSettingKey);
            });
            services.AddSingleton<IChannelFactory, ChannelFactory>();
            services.AddSingleton<IEngineFactory, EngineFactory>();
            services.AddSingleton<IPositionFactory, PositionFactory>();

            services.AddSingleton<IMonitorLoop, MonitorLoop>();
            services.AddHostedService<MonitorDeviceHostedService>();
            services.AddHostedService<MonitorPositionHostedService>();

            return services;
        }
    }
}
