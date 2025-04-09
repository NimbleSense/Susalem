using AutoMapper;
using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.DTOs.Role;
using Susalem.Core.Application.ReadModels.User;
using Susalem.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Susalem.Infrastructure.Mappings
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequestDTO, ApplicationUser>();

            CreateMap<ApplicationUser, UserReadModel>();

            CreateMap<RoleRequestDTO, IdentityRole>();

            CreateMap<IdentityRole, RoleReadModel>();

            CreateMap<IdentityError, ResultError>()
                .ForMember(target => target.Error, opt => opt.MapFrom(source => source.Description));
        }
    }
}
