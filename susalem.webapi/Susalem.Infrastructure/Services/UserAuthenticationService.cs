using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Susalem.Infrastructure.Services
{
    public class UserAuthenticationService :IUserAuthenticationService
    {
        private readonly ILogger<UserAuthenticationService> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtFactory _jwtFactory;

        public UserAuthenticationService(ILogger<UserAuthenticationService> logger,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IJwtFactory jwtFactory)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtFactory = jwtFactory;
        }

        public async Task<Result<AuthenticationModel>> Login(LoginRequestDTO loginRequest)
        {
            var serviceResult = new Result<AuthenticationModel>();
            try
            {
                var applicationUser = await _userManager.FindByNameAsync(loginRequest?.UserName);
                var validationResult = ValidateUserForLogin(applicationUser);
                if (validationResult.Failed)
                {
                    return validationResult;
                }

                return await SignInUser(applicationUser, loginRequest);
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult,_logger,ex,"Error while trying to sign in user.");
            }

            return serviceResult;
        }

        private Result<AuthenticationModel> ValidateUserForLogin(ApplicationUser applicationUser)
        {
            var serviceResult = new Result<AuthenticationModel>();
            if (applicationUser == null)
            {
                return serviceResult.Failed().WithMessage("User not found.");
            }

            if (!applicationUser.IsActive)
            {
                return serviceResult.Failed().WithMessage("User is not active.");
            }

            return serviceResult;
        }

        private async Task<Result<AuthenticationModel>> SignInUser(ApplicationUser applicationUser, LoginRequestDTO loginRequest)
        {
            var serviceResult = new Result<AuthenticationModel>();
            var loginResult = await _signInManager.CheckPasswordSignInAsync(applicationUser, loginRequest.Password, false);
            if (loginResult.Succeeded)
            {
                var authenticationModel = new AuthenticationModel()
                {
                    UserName = applicationUser.UserName
                };
                
                var userClaims = await _userManager.GetClaimsAsync(applicationUser);
                foreach(var t in userClaims)
                {
                    authenticationModel.Permissions.Add(t.Value);
                }

                var roleNames = await _userManager.GetRolesAsync(applicationUser);
                foreach (var roleName in roleNames)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {
                        var roleClaims = await _roleManager.GetClaimsAsync(role);
                        foreach (var t in roleClaims)
                        {
                            authenticationModel.Permissions.Add(t.Value);
                        }
                    }
                }
                GenerateJwtToken(authenticationModel);
                serviceResult.Successful().WithData(authenticationModel);
            }
            else
            {
                serviceResult.Failed().WithMessage("Unable to login.");
            }

            return serviceResult;
        }

        private void GenerateJwtToken(AuthenticationModel model)
        {
            ClaimsIdentity identity;
            try
            {
                identity = _signInManager.Context?.User?.Identities.FirstOrDefault();

            }
            catch (Exception ex)
            {
                identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, model.UserName));
            }

            if (identity == null) return;

            foreach (var permission in model.Permissions)
            {
                identity.AddClaim(new Claim(Permissions.Name, permission));
            }
            model.Token =  _jwtFactory.GenerateJwtToken(identity);
        }

        public async Task<Result<string>> Logout()
        {
            var serviceResult = new Result<string>();
            try
            {
                await _signInManager.SignOutAsync();
                serviceResult.Successful().WithMessage("Successfully logged out.");
            }
            catch (Exception ex)
            {
                ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to sign out user.");
            }

            return serviceResult;
        }
    }
}
