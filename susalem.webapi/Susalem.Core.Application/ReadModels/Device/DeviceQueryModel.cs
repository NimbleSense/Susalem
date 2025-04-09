using System.Collections.Generic;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;

namespace Susalem.Core.Application.ReadModels.Device
{
    /// <summary>
    /// Basic device query model
    /// </summary>
    public class DeviceQueryModel
    {
        public int Id { get; set; }

        public byte Address { get; set; }

        public string Name { get; set; }

        public int DeviceTypeId { get; set; }

        public DeviceTypeEnum DeviceTypeName { get; set; }

        public List<DeviceFunctionProperty> Properties { get; set; } = new List<DeviceFunctionProperty>();

        public string ChannelName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not DeviceQueryModel model)
            {
                return false;
            }

            return model.Id == Id;
        }
    }
}
