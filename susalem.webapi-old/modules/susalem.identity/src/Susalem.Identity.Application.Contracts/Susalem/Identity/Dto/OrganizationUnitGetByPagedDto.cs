using Volo.Abp.Application.Dtos;

namespace Susalem.Identity.Dto
{
    public class OrganizationUnitGetByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
