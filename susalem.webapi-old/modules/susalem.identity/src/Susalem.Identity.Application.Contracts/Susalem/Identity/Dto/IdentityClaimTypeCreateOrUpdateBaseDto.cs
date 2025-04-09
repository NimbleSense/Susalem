using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Susalem.Identity.Dto
{
    public class IdentityClaimTypeCreateOrUpdateBaseDto : ExtensibleObject
    {
        public bool Required { get; set; }

        [DynamicStringLength(typeof(IdentityClaimTypeConsts), nameof(IdentityClaimTypeConsts.MaxRegexLength))]
        public string Regex { get; set; }

        [DynamicStringLength(typeof(IdentityClaimTypeConsts), nameof(IdentityClaimTypeConsts.MaxRegexDescriptionLength))]
        public string RegexDescription { get; set; }

        [DynamicStringLength(typeof(IdentityClaimTypeConsts), nameof(IdentityClaimTypeConsts.MaxDescriptionLength))]
        public string Description { get; set; }


    }
}
