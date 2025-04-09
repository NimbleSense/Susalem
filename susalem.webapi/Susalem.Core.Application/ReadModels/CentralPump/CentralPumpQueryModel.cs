using System.Collections.Generic;
using Susalem.Core.Application.DTOs;

namespace Susalem.Core.Application.ReadModels.CentralPump
{
    public class CentralPumpQueryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte Address { get; set; }

        public IEnumerable<DeviceTypeProperty> Functions { get; set; } = new List<DeviceTypeProperty>();
    }
}
