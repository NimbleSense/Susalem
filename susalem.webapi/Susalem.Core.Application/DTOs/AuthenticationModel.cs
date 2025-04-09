using System.Collections.Generic;

namespace Susalem.Core.Application.DTOs
{
    public class AuthenticationModel
    {
        public bool IsAuthenticated { get; set; }

        public string UserName { get; set; }

        public List<string> Permissions { get; set; } = new List<string>();

        public string Token { get; set; }
    }
}
