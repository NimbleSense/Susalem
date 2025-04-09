using System.Collections.Generic;
using Susalem.Core.Application.DTOs;
using Susalem.Persistence.Models.Application;

namespace Susalem.Infrastructure.Models.Application
{
    public class DeviceEntity : DataEntityBase<int>
    {
        public byte Address { get; set; }

        public string Name { get; set; }

        public ICollection<DeviceFunctionProperty> Properties { get; set; }

        /// <summary>
        /// FK for DeviceType which this device type is
        /// </summary>
        public int DeviceTypeId { get; set; }

        /// <summary>
        /// Navigation property for this device's Type.
        /// </summary>
        public virtual DeviceTypeEntity DeviceType { get; set; }

        public virtual ChannelDevicesEntity ChannelDevices { get; set; }

        //public virtual AlerterEntity Alerter { get; set; }

        //public virtual CentralPumpEntity CentralPump { get; set; }
    }
}
