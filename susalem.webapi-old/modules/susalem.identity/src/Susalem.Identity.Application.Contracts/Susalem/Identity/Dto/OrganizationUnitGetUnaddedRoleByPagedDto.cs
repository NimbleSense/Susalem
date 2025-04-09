using Volo.Abp.Application.Dtos;

namespace Susalem.Identity.Dto
{
    public class OrganizationUnitGetUnaddedRoleByPagedDto : PagedAndSortedResultRequestDto
    {

        public string Filter { get; set; }
    }
}
