using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Susalem.Core.Application.Localize;
using Susalem.Core.Application;

namespace Susalem.Api.Controllers
{
    /// <summary>
    /// Related with user management.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = Roles.UserManagement)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IStringLocalizer<Resource> _localizer;

        public UserController(IApplicationUserService applicationUserService,
            IStringLocalizer<Resource> localizer)
        {
            _applicationUserService = applicationUserService;
            _localizer = localizer;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var serviceResult = await _applicationUserService.GetAllUsersAsync();
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Get user by name
        /// </summary>
        /// <param name="username">user name</param>
        /// <returns></returns>
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser(string username)
        {
            var serviceResult = await _applicationUserService.GetUserAsync(username);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Get user roles
        /// </summary>
        /// <param name="username">user name</param>
        /// <returns></returns>
        [HttpGet("{username}/roles")]
        public async Task<IActionResult> GetRole(string username)
        {
            var serviceResult = await _applicationUserService.GetUserRolesAsync(username);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Get user settable roles
        /// </summary>
        /// <returns></returns>
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var serviceResult = await _applicationUserService.GetRolesAsync();
            if (serviceResult.Failed)
            {
                return NotFound(serviceResult.MessageWithErrors);
            }

            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="requestDTO">User request DTO</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRequestDTO requestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload");
            }
            var serviceResult = await _applicationUserService.CreateUserAsync(requestDTO);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }
            var newDto = serviceResult.Data;
            return CreatedAtAction(nameof(GetUser), new { username = newDto.UserName }, newDto);
        }

        /// <summary>
        /// Update user detail
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="requestDTO">user request DTO</param>
        /// <returns></returns>
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(string userId, [FromBody] UserRequestDTO requestDTO)
        {
            var serviceResult = await _applicationUserService.EditUserAsync(userId, requestDTO);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }
            return NoContent();
        }

        /// <summary>
        /// Activate user
        /// </summary>
        /// <param name="username">user name</param>
        [HttpPost("{username}/activate")]
        public async Task<IActionResult> Activate(string username)
        {
            var serviceResult = await _applicationUserService.ActivateUserAsync(username);
            if (serviceResult.Failed)
            {
                return NotFound(serviceResult.MessageWithErrors);
            }

            return NoContent();
        }

        /// <summary>
        /// DeActivate user
        /// </summary>
        /// <param name="username">User name</param>
        [HttpPost("{username}/deactivate")]
        public async Task<IActionResult> DeActivate(string username)
        {
            var serviceResult = await _applicationUserService.DeactivateUserAsync(username);
            if (serviceResult.Failed)
            {
                return NotFound(serviceResult.MessageWithErrors);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="username">user name</param>
        /// <returns></returns>
        [HttpDelete("{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            var serviceResult = await _applicationUserService.DeleteUserAsync(username);
            if (serviceResult.Failed)
            {
                return NotFound(serviceResult.MessageWithErrors);
            }
            return NoContent();
        }

        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="username">user name</param>
        /// <returns></returns>
        [HttpPost("{username}/reset")]
        public async Task<IActionResult> ResetPassword(string username)
        {
            var serviceResult = await _applicationUserService.ResetPasswordAsync(username);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return NoContent();
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="username">user name</param>
        /// <param name="passwordDto">password</param>
        /// <returns></returns>
        [HttpPost("{username}/password")]
        public async Task<IActionResult> ChangePassword(string username, [FromBody] NewPasswordDTO passwordDto)
        {
            var serviceResult = await _applicationUserService.ChangePasswordAsync(username, passwordDto.NewPassword);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }
            return NoContent();
        }
    }
}
