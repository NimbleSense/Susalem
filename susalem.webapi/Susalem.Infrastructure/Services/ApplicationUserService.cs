using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.DTOs.Role;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.User;
using Susalem.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Susalem.Infrastructure.Services
{
    internal class ApplicationUserService : IApplicationUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IApplicationConfigurationService _configurationService;
        private readonly ILogger<ApplicationUserService> _logger;

        public ApplicationUserService(SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IApplicationConfigurationService configurationService,
            ILogger<ApplicationUserService> logger)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _configurationService = configurationService;
            _logger = logger;
        }

        #region User

        public async Task<Result<UserReadModel>> CreateUserAsync(UserRequestDTO createUserRequestDto)
        {
            var serviceResult = new Result<UserReadModel>();
            try
            {
                var applicationUser = _mapper.Map<ApplicationUser>(createUserRequestDto);
                applicationUser.Id = Guid.NewGuid().ToString();

                var identityResult = await _userManager.CreateAsync(applicationUser, createUserRequestDto.Password);

                if (!identityResult.Succeeded)
                {
                    serviceResult.Failed().WithErrors(_mapper.Map<List<ResultError>>(identityResult.Errors));
                }
                else
                {
                    var rolesResult =
                        await _userManager.AddToRoleAsync(applicationUser, createUserRequestDto.RoleName);
                    if (!rolesResult.Succeeded)
                    {
                        return serviceResult.Failed().WithErrors(_mapper.Map<List<ResultError>>(identityResult.Errors));
                    }
                }

                var userReadModel = _mapper.Map<UserReadModel>(applicationUser);
                serviceResult.Successful().WithData(userReadModel);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to create user.");
            }
            return serviceResult;
        }

        public async Task<Result> ActivateUserAsync(string username)
        {
            var serviceResult = new Result();
            try
            {
                var applicationUser = await _userManager.FindByNameAsync(username);

                if (applicationUser == null)
                    return serviceResult.Failed().WithMessage("User not found.");

                if (applicationUser.IsActive)
                    return serviceResult.Failed().WithMessage("User is already active.");

                applicationUser.IsActive = true;
                var result = await _userManager.UpdateAsync(applicationUser);

                if (result.Succeeded)
                    serviceResult.Successful();
                else
                    serviceResult.Failed().WithMessage($"Unable to activate user.");
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to activate user.");
            }
            return serviceResult;
        }

        public async Task<Result> DeactivateUserAsync(string username)
        {
            var serviceResult = new Result();
            try
            {
                var applicationUser = await _userManager.FindByNameAsync(username);

                if (applicationUser == null)
                    return serviceResult.Failed().WithMessage("User not found.");

                if (!applicationUser.IsActive)
                    return serviceResult.Failed().WithMessage("User is not active.");

                applicationUser.IsActive = false;
                var result = await _userManager.UpdateAsync(applicationUser);

                if (result.Succeeded)
                    serviceResult.Successful();
                else
                    serviceResult.Failed().WithMessage($"Unable to deactivate user.");
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to deactivate user.");
            }

            return serviceResult;
        }

        public async Task<Result<IEnumerable<UserReadModel>>> GetAllUsersAsync()
        {
            var serviceResult = new Result<IEnumerable<UserReadModel>>();
            try
            {
                var applicationUsers = await _userManager.Users.AsNoTracking().Where(u => u.UserName != Configuration.SuperAdminName).ToListAsync();

                var userReadModels = new List<UserReadModel>();
                foreach (var user in applicationUsers)
                {
                    var userReadModel = _mapper.Map<UserReadModel>(user);
                    var roles = await _userManager.GetRolesAsync(user);
                    userReadModel.RoleName = roles.FirstOrDefault();

                    userReadModels.Add(userReadModel);
                }

                serviceResult.Data = userReadModels;
                serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to Get users.");
            }
            return serviceResult;
        }

        public async Task<Result<UserReadModel>> GetUserAsync(string username)
        {
            var serviceResult = new Result<UserReadModel>();
            try
            {
                var applicationUser = await _userManager.FindByNameAsync(username);
                if (applicationUser == null)
                {
                    return serviceResult.Failed().WithMessage("User not found");
                }
                var roles = await _userManager.GetRolesAsync(applicationUser);
                var userReadModel = _mapper.Map<UserReadModel>(applicationUser);
                userReadModel.RoleName = roles.FirstOrDefault();

                serviceResult.Data = userReadModel;
                serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to Get users.");
            }
            return serviceResult;
        }

        public async Task<Result> EditUserAsync(string userId, UserRequestDTO createUserRequestDto)
        {
            var serviceResult = new Result();
            try
            {
                var applicationUser = await _userManager.FindByIdAsync(userId);

                if (applicationUser == null)
                    return serviceResult.Failed().WithMessage("User not found.");

                var updatedUser = _mapper.Map(createUserRequestDto, applicationUser);

                var result = await _userManager.UpdateAsync(updatedUser);

                var existingRoles = await _userManager.GetRolesAsync(applicationUser);
                var identityResult = await _userManager.RemoveFromRolesAsync(applicationUser, existingRoles);

                identityResult = await _userManager.AddToRolesAsync(applicationUser, new List<string> { createUserRequestDto.RoleName });

                if (identityResult.Succeeded)
                    serviceResult.Successful();
                else
                    serviceResult.Failed().WithMessage($"Unable to update user roles.");

                if (result.Succeeded)
                    serviceResult.Successful();
                else
                    serviceResult.Failed().WithMessage($"Unable to update user details.");
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to edit user.");
            }

            return serviceResult;
        }

        public async Task<Result> ChangePasswordAsync(string username, string password)
        {
            //var passwordHasher = new PasswordHasher<ApplicationUser>();
            var serviceResult = new Result();
            try
            {
                var applicationUser = await _userManager.FindByNameAsync(username);
                if (applicationUser == null)
                {
                    return serviceResult.Failed().WithMessage("User not found.");
                }

                //var hashResult = passwordHasher.VerifyHashedPassword(applicationUser, applicationUser.PasswordHash, password);
                //if (hashResult == PasswordVerificationResult.Success)
                //{
                //    return serviceResult.Failed().WithMessage("Need to use different password");
                //}

                var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
                var result = await _userManager.ResetPasswordAsync(applicationUser, token, password);

                if (result.Succeeded)
                {
                    //await _userManager.UpdateAsync(applicationUser);
                    //await _signInManager.SignOutAsync();
                    //await _signInManager.PasswordSignInAsync(applicationUser, password, false, false);
                    serviceResult.Successful();
                }
                else
                {
                    serviceResult.Failed().WithMessage($"Unable to change user password.");
                }
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to change user password.");
            }
            return serviceResult;
        }

        //public async Task<Result> UpdateRolesAsync(string username, List<string> roles)
        //{
        //    var serviceResult = new Result();
        //    try
        //    {
        //        var applicationUser = await _userManager.FindByNameAsync(username);
        //        if (applicationUser == null)
        //        {
        //            return serviceResult.Failed().WithMessage("User not found.");
        //        }

        //        var existingRoles = await _userManager.GetRolesAsync(applicationUser);
        //        var identityResult = await _userManager.RemoveFromRolesAsync(applicationUser, existingRoles);

        //        if (!identityResult.Succeeded) return serviceResult.Failed().WithMessage($"Unable to update user roles.");

        //        identityResult = await _userManager.AddToRolesAsync(applicationUser, roles.Select(r => r.ToUpper()));

        //        if (identityResult.Succeeded)
        //            serviceResult.Successful();
        //        else
        //            serviceResult.Failed().WithMessage($"Unable to update user roles.");
        //    }
        //    catch (Exception ex)
        //    {
        //        ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to update roles of user.");
        //    }

        //    return serviceResult;
        //}

        public async Task<Result> ResetPasswordAsync(string username)
        {
            var defaultPwdResult = _configurationService.GetValue(Configuration.DefaultPwdKey);
            if (defaultPwdResult.Failed)
            {
                return new Result().WithMessage("Get default password failed");
            }

            return await ChangePasswordAsync(username, defaultPwdResult.Data);
        }

        public async Task<Result<IList<string>>> GetUserRolesAsync(string username)
        {
            var serviceResult = new Result<IList<string>>();
            try
            {
                var applicationUser = await _userManager.FindByNameAsync(username);
                if (applicationUser == null)
                {
                    return serviceResult.Failed().WithMessage("User not found.");
                }

                var roles = await _userManager.GetRolesAsync(applicationUser);
                serviceResult.Successful().WithData(roles);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get user roles.");
            }

            return serviceResult;
        }

        public async Task<Result> DeleteUserAsync(string username)
        {
            var serviceResult = new Result();
            try
            {
                var applicationUser = await _userManager.FindByNameAsync(username);
                if (applicationUser == null)
                {
                    return serviceResult.Failed().WithMessage("User not found.");
                }

                var userRoles = await _userManager.GetRolesAsync(applicationUser);
                if (userRoles.Contains(Roles.RootManagement))
                {
                    return serviceResult.Failed().WithMessage("Root user can not delete.");
                }

                var identityResult = await _userManager.DeleteAsync(applicationUser);
                if (identityResult.Succeeded)
                {
                    return serviceResult.Successful();
                }
                return serviceResult.Failed().WithError("Unable to delete user");
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to delete user");
                return serviceResult;
            }
        }

        #endregion

        #region Role

        public async Task<Result<RoleReadModel>> CreateRoleAsync(RoleRequestDTO createRoleRequestDto)
        {
            var serviceResult = new Result<RoleReadModel>();
            try
            {
                var applicationRole = _mapper.Map<IdentityRole>(createRoleRequestDto);
                applicationRole.Id = Guid.NewGuid().ToString();

                var identityResult = await _roleManager.CreateAsync(applicationRole);

                if (!identityResult.Succeeded)
                {
                    serviceResult.Failed().WithErrors(_mapper.Map<List<ResultError>>(identityResult.Errors));
                }
                else if (createRoleRequestDto.Permissions?.Any() ?? false)
                {
                    createRoleRequestDto.Permissions.ForEach(async t => await _roleManager.AddClaimAsync(applicationRole, new System.Security.Claims.Claim(ClaimTypes.Name, t)));
                }

                var roleReadModel = _mapper.Map<RoleReadModel>(applicationRole);
                serviceResult.Successful().WithData(roleReadModel);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to create role.");
            }
            return serviceResult;
        }

        public async Task<Result> DeleteRoleAsync(string roleId)
        {
            var serviceResult = new Result();
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    return serviceResult.Failed().WithMessage("Role not found.");
                }

                var identityResult = await _roleManager.DeleteAsync(role);
                if (identityResult.Succeeded)
                {
                    return serviceResult.Successful();
                }
                return serviceResult.Failed().WithError("Unable to delete role");
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to delete role");
                return serviceResult;
            }
        }

        public async Task<Result<List<RoleReadModel>>> GetRolesAsync()
        {
            var serviceResult = new Result<List<RoleReadModel>>();
            try
            {
                var roleReadModels = new List<RoleReadModel>();
                var roles = await _roleManager.Roles.ToListAsync();

                foreach (var role in roles)
                {
                    var roleReadModel = _mapper.Map<RoleReadModel>(role);
                    var claims = await _roleManager.GetClaimsAsync(role);
                    foreach(var t in claims)
                    {
                        roleReadModel.Permissions.Add(t.Value);
                    }
                    //claims.ForEach(t => roleReadModel.Permissions.Add(t.Value));
                    roleReadModels.Add(roleReadModel);
                }
                serviceResult.Successful().WithData(roleReadModels);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to get roles");
            }

            return serviceResult;
        }

        public async Task<Result> EditRoleAsync(string roleId, RoleRequestDTO createRoleRequestDto)
        {
            var serviceResult = new Result();
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                if (role == null)
                    return serviceResult.Failed().WithMessage("Role not found.");

                var roleClaims = await _roleManager.GetClaimsAsync(role);
                foreach (var roleClaim in roleClaims)
                {
                    await _roleManager.RemoveClaimAsync(role, roleClaim);
                }

                var updatedRole = _mapper.Map(createRoleRequestDto, role);

                var result = await _roleManager.UpdateAsync(updatedRole);

                createRoleRequestDto.Permissions.ForEach(async t =>
                {
                    await _roleManager.AddClaimAsync(updatedRole, new Claim(Permissions.Name, t));
                });

               if (result.Succeeded)
                    serviceResult.Successful();
               else
                    serviceResult.Failed().WithMessage($"Unable to update role details.");
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to edit role.");
            }

            return serviceResult;
        }

        public async Task<Result<RoleReadModel>> GetRoleAsync(string roleId)
        {
            var serviceResult = new Result<RoleReadModel>();
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    return serviceResult.Failed().WithMessage("role not found");
                }
                var roleReadModel = _mapper.Map<RoleReadModel>(role);
                var claims = await _roleManager.GetClaimsAsync(role);

                foreach(var t in claims)
                {
                    roleReadModel.Permissions.Add(t.Value);
                }
                //claims.ForEach(t => roleReadModel.Permissions.Add(t.Value));

                serviceResult.Data = roleReadModel;
                serviceResult.Successful();
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to Get role by id.");
            }
            return serviceResult;
        }

        #endregion
    }
}
