using System;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Identity.Dto
{
    public class IdentityRoleAddOrRemoveOrganizationUnitDto
    {
        [Required]
        public Guid[] OrganizationUnitIds { get; set; }
    }
}
