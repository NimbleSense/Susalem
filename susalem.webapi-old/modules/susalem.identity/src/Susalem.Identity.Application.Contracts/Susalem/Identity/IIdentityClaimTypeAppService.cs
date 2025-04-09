using System;
using System.Threading.Tasks;
using Susalem.Identity.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Susalem.Identity
{
    public interface IIdentityClaimTypeAppService :
        ICrudAppService<
            IdentityClaimTypeDto,
            Guid,
            IdentityClaimTypeGetByPagedDto,
            IdentityClaimTypeCreateDto,
            IdentityClaimTypeUpdateDto>
    {
        Task<ListResultDto<IdentityClaimTypeDto>> GetAllListAsync();
    }
}
