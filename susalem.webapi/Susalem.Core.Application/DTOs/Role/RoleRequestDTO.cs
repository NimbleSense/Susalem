using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Core.Application.DTOs.Role
{
    /// <summary>
    /// Create role request DTO
    /// </summary>
    public class RoleRequestDTO
    {
        /// <summary>
        /// 简称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
