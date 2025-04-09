using System.Collections.Generic;
using Susalem.Core.Application.DTOs;

namespace Susalem.Core.Application.ReadModels.Alerter
{
    public class AlerterQueryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte Address { get; set; }

        public bool IsEnableBuzzing { get; set; }

        public IEnumerable<DeviceTypeProperty> Functions { get; set; } = new List<DeviceTypeProperty>();

        public IEnumerable<AlerterDevice> AffectedDevices { get; set; } = new List<AlerterDevice>();

        public int DeviceId { get; set; }
    }

    public class AlerterMonitorQueryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }
        public string Lighting { get; set; }

        public bool IsEnableBuzzing { get; set; }
    }

    public class AlerterInfoQueryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsEnableBuzzing { get; set; }
        
        public IEnumerable<AlerterDevice> AffectedDevices { get; set; } = new List<AlerterDevice>();
    }

    public class AlerterDevice
    {
        public int DeviceId { get; set; }
    }
}
