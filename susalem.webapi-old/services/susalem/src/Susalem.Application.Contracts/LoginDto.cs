using System.ComponentModel.DataAnnotations;

namespace Susalem;

public class LoginDto
{
    [Required]
    [StringLength(255)]
    public string Username { get; set; }

    [Required]
    [StringLength(32)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}