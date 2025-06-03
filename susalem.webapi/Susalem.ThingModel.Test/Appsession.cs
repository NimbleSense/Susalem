using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.ThingModel.Interface;
using Susalem.Infrastructure.ThingModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.ThingModel.Test
{
    public class Appsession
    {
        public static List<ThingObject> Devices { get; set; } = new List<ThingObject>();

        public static Dictionary<string, List<ThingCommandDto>> ThingCommands = new Dictionary<string, List<ThingCommandDto>>();

        public static List<IThingObjectDriver> MonitorDrivers { get; } = new List<IThingObjectDriver>();
    }
}
