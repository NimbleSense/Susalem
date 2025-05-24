using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.ThingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.ThingModel.Test
{
    public class Appsession
    {
        public static List<ThingObject> Devices { get; } = new List<ThingObject>();

        public static List<IMonitorDriver> MonitorDrivers { get; } = new List<IMonitorDriver>();
    }
}
