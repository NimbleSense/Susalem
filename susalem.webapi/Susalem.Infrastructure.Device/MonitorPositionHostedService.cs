using Susalem.Core.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Susalem.Core.Application.Commands.Monitor;
using System.Linq;
using Susalem.Core.Application.Models;

namespace Susalem.Infrastructure.Device;

public class MonitorPositionHostedService : BackgroundService
{
    private readonly IServiceBus _serviceBus;
    private readonly IPositionFactory _positionFactory;
    private readonly MonitorSetting _monitorSetting;
    private readonly ILogger<MonitorPositionHostedService> _logger;

    public MonitorPositionHostedService(IServiceBus serviceBus, 
        IPositionFactory positionFactory,
        MonitorSetting monitorSetting,
        ILogger<MonitorPositionHostedService> logger)
    {
        _serviceBus = serviceBus;
        _positionFactory = positionFactory;
        _monitorSetting = monitorSetting;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _positionFactory.InitializeAsync();

        _logger.LogInformation("Monitor position hosted service is running");
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
    }

}