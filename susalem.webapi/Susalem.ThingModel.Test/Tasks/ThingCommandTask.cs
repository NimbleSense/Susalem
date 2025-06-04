using Newtonsoft.Json;
using Quartz;
using Susalem.Infrastructure.ThingModel;
using Susalem.Infrastructure.ThingModel.Interface;
using Susalem.Infrastructure.ThingModel.Model;
using Susalem.Messages.Features.Channel;
using Susalem.ThingModel.Test.MobudsThing;
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
        public ThingCommandTask()
        {
            foreach(var item in Appsession.DictReadIoModels)
            {
                string key = item.Key;
                IModbusThingDriver modbusThingDriver = Appsession.MonitorDrivers[key];
                for (int i = 0;i<item.Value.Count;i++)
                {
                    var ioModel = item.Value[i];
                    if(ioModel.PropertyKeys.Length==1)
                    {
                        Property pro = Appsession.Devices[key].Properties.Find(x=>x.Key==ioModel.PropertyKeys[0]);
                        pro.CurrentValue = modbusThingDriver.Read(ioModel, false);
                    }
                    else
                    {
                        ModbusThingRetModel model =  modbusThingDriver.Read(ioModel, true);
                        ushort[] value = (ushort[])model.Value;
                        // 多地址混合读取
                        for (int j=0;j< ioModel.PropertyKeys.Length;j++)
                        {
                            Property pro = Appsession.Devices[key].Properties.Find(x => x.Key == ioModel.PropertyKeys[j]);
                            if(j==0)
                            {
                                ushort[] newValue = new ushort[ioModel.BatchLength[j]];
                                Array.Copy(value, 0, newValue, 0, ioModel.BatchLength[j]);

                            }
                            else
                            {
                                ushort[] newValue = new ushort[value[ioModel.BatchLength[j]]];
                                Array.Copy(value, ioModel.BatchLength[j], newValue, 0, value[ioModel.BatchLength[j]]);
                            }
                            //pro.CurrentValue = value[ioModel.BatchLength[j]
                        }
                        

                    }
                }
            }
        }
        public async Task Execute(IJobExecutionContext context)
        {
            //for (int i = 0; i < Appsession.ThingCommands.Count; i++)
            //{

            //}
        }
    }
}
