using System;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Identity.Dto
{
    public class IdentityUserOrganizationUnitUpdateDto
    {
        [Required]
        public Guid[] OrganizationUnitIds { get; set; }
    }
}
