using System.Collections.Generic;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;

namespace Susalem.Infrastructure.Models.Application
{
    public class DeviceTypeEntity : DataEntityBase<int>
    {
        public DeviceTypeEnum Name { get; set; }

        public string Description { get; set; }

        public bool IsPublish { get; set; }

        public ICollection<DeviceTypeProperty> Properties { get; set; }

        public virtual ICollection<DeviceEntity> Devices { get; set; } = new List<DeviceEntity>();
    }
}
