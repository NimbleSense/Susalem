using System.ComponentModel.DataAnnotations;

namespace Susalem.Core.Application.DTOs
{
    /// <summary>
    /// User login request 
    /// </summary>
    public class LoginRequestDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
