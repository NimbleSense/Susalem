using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Core.Application.DTOs
{
    /// <summary>
    /// Device request data transfer object
    /// </summary>
    public class DeviceRequestDTO
    {
        /// <summary>
        /// Display name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Communicate address
        /// </summary>
        [Required]
        public byte Address { get; set; }

        /// <summary>
        /// Belongs to which device type
        /// </summary>
        [Required]
        public int DeviceTypeId { get; set; }

        /// <summary>
        /// Device function properties.
        /// </summary>
        [Required]
        public List<DeviceFunctionProperty> Properties { get; set; }
    }
}
