using System.ComponentModel.DataAnnotations;

namespace Susalem.Core.Application.DTOs
{
    /// <summary>
    /// Area DTO
    /// </summary>
    public class AreaDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
