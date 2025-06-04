using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.ThingModel.Interface;
using Susalem.Infrastructure.ThingModel.Model;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.ThingModel.Test
{
    public class Appsession
    {

        public static Dictionary<string, ThingObject> Devices { get; } = new Dictionary<string, ThingObject>();

        public static Dictionary<string, List<ModbusThingIoModel>> DictReadIoModels { get; set; } = new Dictionary<string, List<ModbusThingIoModel>>();

        public static Dictionary<string, List<ModbusCommandIoModel>> DictCommandIoModels { get; set; } =new Dictionary<string, List<ModbusCommandIoModel>>();

        public static Dictionary<string, IModbusThingDriver> MonitorDrivers { get; } = new Dictionary<string, IModbusThingDriver>();
    }
}
