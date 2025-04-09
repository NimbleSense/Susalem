using System;
using System.Threading;
using System.Threading.Tasks;
using Susalem.Core.Application.Interfaces;
using Susalem.Messages.Enumerations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Susalem.Infrastructure.Device;

/// <summary>
/// 设备通信遍历数据
/// </summary>
public class MonitorDeviceHostedService : BackgroundService
{
    private readonly IEngineFactory _engineFactory;
    private readonly IChannelFactory _channelFactory;
    private readonly ILogger<MonitorDeviceHostedService> _logger;

    public MonitorDeviceHostedService(IChannelFactory channelFactory,
        IEngineFactory engineFactory,
        ILogger<MonitorDeviceHostedService> logger)                             
    {
        _channelFactory= channelFactory;
        _engineFactory = engineFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _channelFactory.InitializeAsync();
        await _engineFactory.InitializeAsync();

        _logger.LogInformation("Monitor device hosted service is running");

        foreach(var channel in _channelFactory.Channels)
        {
            if (!channel.Channel.Enable)
                continue;

            new Thread(() =>
            {
                BackgroundProcessing(channel, stoppingToken);
            }).Start();
        }
    }

    private void BackgroundProcessing(ICommChannel channel, CancellationToken stoppingToken)
    {
        var engines = _engineFactory.GetEnginesByChannel(channel.Channel.Id);
        _logger.LogInformation("Channel: {ChannelName}, Content:{Content}, Device Count:{DeviceCount}", 
            channel.Channel.Name, 
            channel.Channel.Content,
            engines.Count);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (channel.Status == ChannelStatus.Offline)
                {
                    if (!channel.MonitorDriver.Connect())
                    {
                        _logger.LogError("Monitor driver {ChannelName} connect failed, after 5s will retry connect", channel.Channel.Name);
                        Thread.Sleep(5 * 1000);
                        continue;
                    }
                }
                var deviceInterval = channel.Channel.Settings.DeviceInterval;
                if (deviceInterval <= 0)
                {
                    deviceInterval = 300; 
                }
                //设备遍历引擎进行更新数据
                //查看设备的运行时间来暂停和恢复设备
                foreach (var deviceEngine in engines)
                {
                    if (channel.Status == ChannelStatus.Offline)
                    {
                        break;    
                    }
                    _logger.LogDebug($"Channel: {channel.Channel.Name}, Device address: {deviceEngine.BasicInfo.Address}");
                    if (deviceEngine.UpdateTelemetries())
                    {
                        foreach (var deviceEngineTelemetry in deviceEngine.Telemetries)
                        {
                            _logger.LogDebug($" {deviceEngineTelemetry.Key} : {deviceEngineTelemetry.OriginalValue} => {deviceEngineTelemetry.Value} ");
                        }
                    }

                    Thread.Sleep(deviceInterval);
                }

                _logger.LogDebug("End Loop Devices");
            }
            catch(Exception ex) 
            {
                _logger.LogError($"Loop devices exception: {ex}");
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Monitor Device Hosted Service is stopping.");

        foreach (var channel in _channelFactory.Channels)
        {
            channel.MonitorDriver.Disconnect();
        }

        await base.StopAsync(cancellationToken);
    }
}