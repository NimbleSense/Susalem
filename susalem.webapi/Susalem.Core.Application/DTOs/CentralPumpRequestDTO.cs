using System.ComponentModel.DataAnnotations;

namespace Susalem.Core.Application.DTOs
{
    public class CentralPumpRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
