using Volo.Abp.Application.Dtos;

namespace Susalem.Identity.Dto
{
    public class OrganizationUnitGetUnaddedUserByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
