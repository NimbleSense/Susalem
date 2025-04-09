using Microsoft.AspNetCore.Identity;

namespace Susalem.Infrastructure.Models.Identity
{
    public class ApplicationUser:IdentityUser
    {
        /// <summary>
        /// Flag indicating whether user is active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
