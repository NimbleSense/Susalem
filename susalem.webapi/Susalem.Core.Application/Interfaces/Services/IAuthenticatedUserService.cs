using System.Collections.Generic;
using System.Security.Claims;

namespace Susalem.Core.Application.Interfaces.Services
{
    public interface IAuthenticatedUserService
    {
        public string Name { get; set; }
        public string UserName { get; set; }

        IList<string> Roles { get; }

    }
}
