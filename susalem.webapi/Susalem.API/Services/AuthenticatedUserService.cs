using System.Collections.Generic;
using System.Security.Claims;
using Susalem.Core.Application;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Susalem.Api.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public string Name { get; set; }
        public string UserName { get; set; }

        public IList<string> Roles { get; }

        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager)
        {
            Name = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? null;
            UserName = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.GivenName)?.Value ?? null;
            Roles = new List<string>();
            var roleClaims = httpContextAccessor.HttpContext?.User.FindAll(Permissions.Name);
            if (roleClaims != null)
            {
                foreach (var roleClaim in roleClaims)
                {
                    Roles.Add(roleClaim.Value);
                }
            }

            _signInManager = signInManager;
        }

        public ClaimsIdentity GetClaimsIdentity()
        {
            throw new System.NotImplementedException();
        }
    }
}
