using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Susalem.Core.Application.Enumerations;

namespace Susalem.Core.Application.DTOs
{
    /// <summary>
    /// Create position request
    /// </summary>
    public class PositionRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int AreaId { get; set; }

        public ICollection<int> BoundedAlerter { get; set; } = new List<int>();

        public string Description { get; set; }

        public bool ShowDoor {  get; set; }
    }

    public class PositionFunctionProperty
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        public PositionFunctionEnum Key { get; set; }

        public ICollection<PositionDeviceProperty> Devices { get; set; } = new List<PositionDeviceProperty>();
    }

    public class PositionDeviceProperty
    {
        /// <summary>
        /// Device function key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Device Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
    }
}
