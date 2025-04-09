using Volo.Abp.ObjectExtending;

namespace Susalem.Identity.Dto
{
    public class OrganizationUnitUpdateDto : ExtensibleObject
    {
        public string DisplayName { get; set; }
    }
}
