using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.Device.Model;
using Susalem.Infrastructure.ThingModel.Interface;
using Susalem.Infrastructure.ThingModel.Model;
using Susalem.Messages.Features.Channel;
using Susalem.ThingModel.Test.MobudsThing;
using System.Configuration;
using System.Threading.Channels;

namespace Susalem.ThingModel.Test
{
    [PersistJobDataAfterExecution, DisallowConcurrentExecution]
    public class ThingConnectTask : IJob
    {
        private readonly ILogger<ThingConnectTask> _logger;
        public IList<ICommChannel> Channels { get; } = new List<ICommChannel>();
        public IList<ThingObject> Devices { get; }

        public ThingConnectTask()
        {
            //Console.WriteLine("Ctor");
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Test");
            for(int i=0;i< Appsession.MonitorDrivers.Keys.Count;i++)
            {
                var key = Appsession.MonitorDrivers.Keys.ElementAt(i); 
                var item = Appsession.MonitorDrivers[key];
                var currentDevice = Appsession.Devices[key];
                if (currentDevice.DeviceCollectionPro == MasterType.Rtu)
                {
                    // TestData
                    if (!item.IsConnected)
                    {
                        var serialSetting = currentDevice.SerialSetting;
                        DeviceModBusMaster master = new DeviceModBusMaster(MasterType.Rtu,serialSetting, _logger);
                        master.Connect();
                        item = master;
                    }
                }
                else if (currentDevice.DeviceCollectionPro == MasterType.Tcp)
                {
                    // TestData
                    if (!item.IsConnected)
                    {
                        var tcpSetting = currentDevice.TcpSetting;
                        DeviceModBusMaster master = new DeviceModBusMaster(MasterType.Rtu, tcpSetting, _logger);
                        master.Connect();
                        item = master;
                    }
                }
            }

            Console.WriteLine("连接设备");
        }
    }
}
