using Microsoft.AspNetCore.Mvc;
using Susalem.Identity.Dto;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Susalem.Identity;

[RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName), Area("identity"), ControllerName("User"), Route("api/identity/users")]
public class IdentityUserController : AbpController, IIdentityUserAppService
{
    public IdentityUserController(
        IIdentityUserAppService userAppService)
    {
        UserAppService = userAppService;
    }

    protected IIdentityUserAppService UserAppService { get; }

    [HttpPut, Route("change-password")]
    public virtual async Task ChangePasswordAsync(Guid id, IdentityUserSetPasswordInput input)
    {
        await UserAppService.ChangePasswordAsync(id, input);
    }

    [HttpPut, Route("change-two-factor")]
    public virtual async Task ChangeTwoFactorEnabledAsync(Guid id, TwoFactorEnabledDto input)
    {
        await UserAppService.ChangeTwoFactorEnabledAsync(id, input);
    }

    [HttpPut, Route("{id}/lock/{seconds}")]
    public virtual async Task LockAsync(Guid id, int seconds)
    {
        await UserAppService.LockAsync(id, seconds);
    }

    [HttpPut, Route("{id}/unlock")]
    public virtual async Task UnLockAsync(Guid id)
    {
        await UserAppService.UnLockAsync(id);
    }

    [HttpGet, Route("profile")]
    public virtual async Task<IdentityUserDto> GetProfileAsync()
    {
        return new()
        {
            UserName = "admin",
            Email = "admin@susalem.io",
            Name = "admin"
        };
    }

    #region OrganizationUnit

    [HttpGet, Route("{id}/organization-units")]
    public virtual async Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync(Guid id)
    {
        return await UserAppService.GetOrganizationUnitsAsync(id);
    }

    [HttpPut, Route("{id}/organization-units")]
    public virtual async Task SetOrganizationUnitsAsync(Guid id, IdentityUserOrganizationUnitUpdateDto input)
    {
        await UserAppService.SetOrganizationUnitsAsync(id, input);
    }

    [HttpDelete, Route("{id}/organization-units/{ouId}")]
    public virtual async Task RemoveOrganizationUnitsAsync(Guid id, Guid ouId)
    {
        await UserAppService.RemoveOrganizationUnitsAsync(id, ouId);
    }

    #endregion

    #region Claim

    [HttpGet, Route("{id}/claims")]
    public virtual async Task<ListResultDto<IdentityClaimDto>> GetClaimsAsync(Guid id)
    {
        return await UserAppService.GetClaimsAsync(id);
    }

    [HttpPost, Route("{id}/claims")]
    public virtual async Task AddClaimAsync(Guid id, IdentityUserClaimCreateDto input)
    {
        await UserAppService.AddClaimAsync(id, input);
    }

    [HttpPut, Route("{id}/claims")]
    public virtual async Task UpdateClaimAsync(Guid id, IdentityUserClaimUpdateDto input)
    {
        await UserAppService.UpdateClaimAsync(id, input);
    }

    [HttpDelete, Route("{id}/claims")]
    public virtual async Task DeleteClaimAsync(Guid id, IdentityUserClaimDeleteDto input)
    {
        await UserAppService.DeleteClaimAsync(id, input);
    }

    #endregion
}