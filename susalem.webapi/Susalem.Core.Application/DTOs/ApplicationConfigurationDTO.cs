using System.ComponentModel.DataAnnotations;

namespace Susalem.Core.Application.DTOs
{
    public class ApplicationConfigurationDTO
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsEncrypted { get; set; }
    }
}
