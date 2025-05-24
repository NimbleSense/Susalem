using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.Device;
using Susalem.Infrastructure.Device.Model;
using Susalem.Infrastructure.Services;
using Susalem.Messages.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Susalem.ThingModel.Test.MobudsThing
{
    public class ModbusThingService:IModbusThingService
    {
        private readonly IChannelFactory _channelFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ModbusThingService> _logger;
        public IList<ICommChannel> Channels { get; } = new List<ICommChannel>();

        public ModbusThingService(IChannelFactory channelFactory, IServiceProvider serviceProvider,ILogger<ModbusThingService> logger)
        {
            _logger = logger;
            _channelFactory = channelFactory;
        }

        public async Task StartMonitor()
        {
            using var scope = _serviceProvider.CreateScope();
            var channelService = scope.ServiceProvider.GetService<IChannelService>();
            var result = await channelService.GetChannelsAsync();
            foreach (var channel in result.Data)
            {
                if (!channel.Enable)
                {
                    _logger.LogWarning($"Channel: {channel.Name} is disabled");
                }

                Channels.Add(new CommChannel(channel));
            }
        }

        private void BackgroundProcessing(ICommChannel channel, CancellationToken stoppingToken)
        {

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    try
            //    {
            //        if (channel.Status == ChannelStatus.Offline)
            //        {
            //            if (!channel.MonitorDriver.Connect())
            //            {
            //                _logger.LogError("Monitor driver {ChannelName} connect failed, after 5s will retry connect", channel.Channel.Name);
            //                Thread.Sleep(5 * 1000);
            //                continue;
            //            }
            //        }
            //        var deviceInterval = channel.Channel.Settings.DeviceInterval;
            //        if (deviceInterval <= 0)
            //        {
            //            deviceInterval = 300;
            //        }
            //        //设备遍历引擎进行更新数据
            //        //查看设备的运行时间来暂停和恢复设备
            //        foreach (var deviceEngine in engines)
            //        {
            //            if (channel.Status == ChannelStatus.Offline)
            //            {
            //                break;
            //            }
            //            _logger.LogDebug($"Channel: {channel.Channel.Name}, Device address: {deviceEngine.BasicInfo.Address}");
            //            if (deviceEngine.UpdateTelemetries())
            //            {
            //                foreach (var deviceEngineTelemetry in deviceEngine.Telemetries)
            //                {
            //                    _logger.LogDebug($" {deviceEngineTelemetry.Key} : {deviceEngineTelemetry.OriginalValue} => {deviceEngineTelemetry.Value} ");
            //                }
            //            }

            //            Thread.Sleep(deviceInterval);
            //        }

            //        _logger.LogDebug("End Loop Devices");
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError($"Loop devices exception: {ex}");
            //    }
            //}
        }
    }
}
