using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Susalem.Core.Application.ReadModels.Alerter;

namespace Susalem.Core.Application.DTOs
{
    /// <summary>
    /// Alerter request DTO
    /// </summary>
    public class AlerterRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsEnableBuzzing { get; set; }

        public ICollection<AlerterDevice> AffectedDevices { get; set; }
    }
}
