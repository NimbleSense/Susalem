using System.Collections.Generic;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;

namespace Susalem.Core.Application.ReadModels.Device
{
    /// <summary>
    /// Device type query model
    /// </summary>
    public class DeviceTypeQueryModel
    {
        /// <summary>
        /// Device type id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Device type name
        /// </summary>
        public DeviceTypeEnum Name { get; set; }

        /// <summary>
        /// Device type description
        /// </summary>
        public string Description { get; set; }

        public List<DeviceTypePropertyQueryModel> Properties { get; set; }
    }

    public class DeviceTypePropertyQueryModel
    {
        public string Key { get; set; }

        public int? Reg { get; set; }

        public int Length { get; set; }

        public int Factor { get; set; }

        /// <summary>
        /// 计算表达式
        /// </summary>
        public string Formula { get; set; }
    }
}
