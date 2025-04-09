using Volo.Abp.Modularity;

namespace Susalem.Identity
{
    [DependsOn(
        typeof(AbpIdentityDomainSharedModule),
        typeof(Volo.Abp.Identity.AbpIdentityDomainModule))]
    public class AbpIdentityDomainModule : AbpModule
    {
    }
}
