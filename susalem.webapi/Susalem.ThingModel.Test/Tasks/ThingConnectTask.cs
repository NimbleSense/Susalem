using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.Device.Model;
using Susalem.Infrastructure.ThingModel;
using Susalem.Messages.Features.Channel;
using System.Threading.Channels;

namespace Susalem.ThingModel.Test
{
    [PersistJobDataAfterExecution, DisallowConcurrentExecution]
    public class ThingConnectTask : IJob
    {
        public ThingObject DeviceModel;
        private readonly IChannelFactory _channelFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ThingConnectTask> _logger;
        public IList<ICommChannel> Channels { get; } = new List<ICommChannel>();
        public IList<ThingObject> Devices { get; }
        public async Task Execute(IJobExecutionContext context)
        {

            //using var scope = _serviceProvider.CreateScope();
            //var channelService = scope.ServiceProvider.GetService<IChannelService>();
            //var result = await channelService.GetChannelsAsync();
            
            for(int i=0;i<Appsession.MonitorDrivers.Count;i++)
            {
                var item = Appsession.MonitorDrivers[i];
                var currentDevice = Appsession.Devices[i];
                if (DeviceModel.DeviceCollectionPro == DeviceCollectionPro.ModbusRtu)
                {
                    // TestData
                    if (!item.IsConnected)
                    {
                        var serialSetting = JsonConvert.DeserializeObject<SerialSetting>(currentDevice.ConnectString);
                        var commSetting = JsonConvert.DeserializeObject<CommonSetting>(currentDevice.CommonSetting);
                        ModbusRtuDeviceBase modbusRtuDeviceBase = new ModbusRtuDeviceBase(serialSetting, commSetting, _logger);
                        modbusRtuDeviceBase.Connect();
                        item = modbusRtuDeviceBase;
                    }
                }
                else if(DeviceModel.DeviceCollectionPro == DeviceCollectionPro.ModbusTcp)
                {
                    // TestData
                    if (!item.IsConnected)
                    {
                        var tcpSetting = JsonConvert.DeserializeObject<TcpSetting>(currentDevice.ConnectString);
                        var commSetting = JsonConvert.DeserializeObject<CommonSetting>(currentDevice.CommonSetting);
                        ModbusTcpDeviceBase modbusTcpDeviceBase = new ModbusTcpDeviceBase(tcpSetting, commSetting, _logger);
                        modbusTcpDeviceBase.Connect();
                    } 
                }


            }

        }
    }
}
