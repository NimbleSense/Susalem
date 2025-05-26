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
        private readonly ILogger<ThingConnectTask> _logger;
        public IList<ICommChannel> Channels { get; } = new List<ICommChannel>();
        public IList<ThingObject> Devices { get; }
        public async Task Execute(IJobExecutionContext context)
        {
            for (int i = 0; i < Appsession.MonitorDrivers.Count; i++)
            {
                var item = Appsession.MonitorDrivers[i];
                var currentDevice = Appsession.Devices[i];
                if (currentDevice.DeviceCollectionPro == DeviceCollectionPro.ModbusRtu)
                {
                    // TestData
                    if (!item.IsConnected)
                    {
                        var serialSetting = JsonConvert.DeserializeObject<SerialSetting>(currentDevice.ConnectString);
                        ModbusRtuDeviceBase modbusRtuDeviceBase = new ModbusRtuDeviceBase(serialSetting, currentDevice.CommonSetting, _logger);
                        modbusRtuDeviceBase.Connect();
                        item = modbusRtuDeviceBase;
                    }
                }
                else if (currentDevice.DeviceCollectionPro == DeviceCollectionPro.ModbusTcp)
                {
                    // TestData
                    if (!item.IsConnected)
                    {
                        var tcpSetting = JsonConvert.DeserializeObject<TcpSetting>(currentDevice.ConnectString);
                        ModbusTcpDeviceBase modbusTcpDeviceBase = new ModbusTcpDeviceBase(tcpSetting, currentDevice.CommonSetting, _logger);
                        modbusTcpDeviceBase.Connect();
                    }
                }


            }

            Console.WriteLine("连接设备");
        }
    }
}
