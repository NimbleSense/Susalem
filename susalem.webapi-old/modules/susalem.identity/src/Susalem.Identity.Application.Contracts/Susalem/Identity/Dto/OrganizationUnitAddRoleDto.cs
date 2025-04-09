using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Identity.Dto
{
    public class OrganizationUnitAddRoleDto
    {
        [Required]
        public List<Guid> RoleIds { get; set; }
    }
}
