using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.Device.Model;
using Susalem.Infrastructure.ThingModel;
using Susalem.Messages.Features.Channel;

namespace Susalem.ThingModel.Test
{
    [PersistJobDataAfterExecution, DisallowConcurrentExecution]
    public class ThingConnectTask : IJob
    {
        public DeviceModel DeviceModel;
        private readonly IChannelFactory _channelFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ThingConnectTask> _logger;
        public IList<ICommChannel> Channels { get; } = new List<ICommChannel>();
        public IList<DeviceModel> Devices { get; }
        public async Task Execute(IJobExecutionContext context)
        {
            // Todo 解析ThingModel 中的Command
            if (DeviceModel == null|| DeviceModel.IsConnect)
                return;
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
                var serialSetting = JsonConvert.DeserializeObject<SerialSetting>(channel.Content);
                if (DeviceModel.DeviceCollectionPro == DeviceCollectionPro.ModbusRtu)
                {
                    ModbusRtuDeviceBase modbusRtuDeviceBase = new ModbusRtuDeviceBase(serialSetting,channel.Settings, _logger);
                    
                }
            }
            
        }
    }
}
