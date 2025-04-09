using System.Collections.Generic;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.ReadModels.Alerter;

namespace Susalem.Infrastructure.Models.Application
{
    /// <summary>
    /// Alerter entity
    /// </summary>
    public class AlerterEntity : DataEntityBase<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 是否开启蜂鸣声
        /// </summary>
        public bool IsEnableBuzzing { get; set; }

        /// <summary>
        /// Contains devices
        /// </summary>
        public ICollection<AlerterDevice> AffectedDevices { get; set; }

        public ICollection<DeviceTypeProperty> Properties { get; set; }

        /// <summary>
        /// Alerter mapping device.
        /// </summary>
        public int DeviceId { get; set; }

        public virtual DeviceEntity Device { get; set; }
    }

}
