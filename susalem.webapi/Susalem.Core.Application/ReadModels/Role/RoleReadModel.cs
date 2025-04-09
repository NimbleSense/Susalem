using System;
using System.Collections.Generic;

namespace Susalem.Core.Application.ReadModels.User
{
    public class RoleReadModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<string> Permissions { get; set; } = new List<string>();
    }
}
