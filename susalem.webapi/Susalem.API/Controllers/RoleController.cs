using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.DTOs.Role;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Localize;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace Susalem.Api.Controllers;

/// <summary>
/// 用户角色控制
/// </summary>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IApplicationUserService _applicationUserService;
    private readonly IStringLocalizer<Resource> _localizer;

    public RoleController(IApplicationUserService applicationUserService, 
        IStringLocalizer<Resource> localizer)
    {
        _applicationUserService = applicationUserService;
        _localizer = localizer;
    }

    /// <summary>
    /// Get all roles
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var serviceResult = await _applicationUserService.GetRolesAsync();
        if (serviceResult.Failed)
        {
            return BadRequest(serviceResult.MessageWithErrors);
        }

        return Ok(serviceResult.Data);
    }

    /// <summary>
    /// Get role by id
    /// </summary>
    /// <param name="roleId">role id</param>
    /// <returns></returns>
    [HttpGet("{roleId}")]
    public async Task<IActionResult> GetRole(string roleId)
    {
        var serviceResult = await _applicationUserService.GetRoleAsync(roleId);
        if (serviceResult.Failed)
        {
            return BadRequest(serviceResult.MessageWithErrors);
        }
        return Ok(serviceResult.Data);
    }

    /// <summary>
    /// Create new role
    /// </summary>
    /// <param name="requestDTO">User request DTO</param>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RoleRequestDTO requestDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid payload");
        }
        var serviceResult = await _applicationUserService.CreateRoleAsync(requestDTO);
        if (serviceResult.Failed)
        {
            return BadRequest(serviceResult.MessageWithErrors);
        }
        var newRoleDto = serviceResult.Data;
        return CreatedAtAction(nameof(GetRole), new { roleId = newRoleDto.Id }, newRoleDto);
    }

    /// <summary>
    /// Update role detail
    /// </summary>
    /// <param name="roleId">role id</param>
    /// <param name="requestDTO">role request DTO</param>
    /// <returns></returns>
    [HttpPut("{roleId}")]
    public async Task<IActionResult> Put(string roleId, [FromBody] RoleRequestDTO requestDTO)
    {
        var serviceResult = await _applicationUserService.EditRoleAsync(roleId, requestDTO);
        if (serviceResult.Failed)
        {
            return BadRequest(serviceResult.MessageWithErrors);
        }
        return NoContent();
    }

    /// <summary>
    /// Delete role
    /// </summary>
    /// <param name="roleId">role id</param>
    /// <param name="roleName">role name</param>
    /// <returns></returns>
    [HttpDelete("{roleId}/{roleName}")]
    public async Task<IActionResult> Delete(string roleId, string roleName)
    {
        var serviceResult = await _applicationUserService.DeleteRoleAsync(roleId);
        if (serviceResult.Failed)
        {
            return NotFound(serviceResult.MessageWithErrors);
        }
        return NoContent();
    }
}
