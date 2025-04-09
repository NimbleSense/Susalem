using System;

namespace Susalem.Core.Application.ReadModels.User
{
    public class UserReadModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public string RoleName { get; set; }
    }
}
