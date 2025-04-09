using Volo.Abp.Application.Dtos;

namespace Susalem.Identity.Dto
{
    public class IdentityClaimTypeGetByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
