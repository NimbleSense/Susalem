using Newtonsoft.Json;
using Quartz;
using Susalem.Messages.Features.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.ThingModel.Test.Tasks
{
    [PersistJobDataAfterExecution, DisallowConcurrentExecution]
    public class ThingCommandTask : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            for (int i = 0; i < Appsession.Devices.Count; i++)
            {
                for (int j = 0; j < Appsession.Devices[i].ReadConfigs.Count; j++)
                {
                    var deviceRead = Appsession.Devices[i].ReadConfigs[j];
                    var monitorDriver = Appsession.MonitorDrivers[i];
                    deviceRead.CurrentValue =
                        monitorDriver.Read(deviceRead.);
                }
            }
        }
    }
}
