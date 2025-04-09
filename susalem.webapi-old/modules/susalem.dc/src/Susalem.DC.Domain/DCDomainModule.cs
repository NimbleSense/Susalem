using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Susalem.DC;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DCDomainSharedModule)
)]
public class DCDomainModule : AbpModule
{

}
