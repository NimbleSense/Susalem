using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Identity.Dto
{
    public class OrganizationUnitAddUserDto
    {
        [Required]
        public List<Guid> UserIds { get; set; }
    }
}
