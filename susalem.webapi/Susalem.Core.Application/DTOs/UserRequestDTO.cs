using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Core.Application.DTOs
{
    /// <summary>
    /// Create user request DTO
    /// </summary>
    public class UserRequestDTO
    {
        /// <summary>
        /// User name (used to login).
        /// </summary>
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User active status.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        public string RoleName { get; set; }
    }
}
