using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Susalem.Mes;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(MesDomainSharedModule)
)]
public class MesDomainModule : AbpModule
{

}
