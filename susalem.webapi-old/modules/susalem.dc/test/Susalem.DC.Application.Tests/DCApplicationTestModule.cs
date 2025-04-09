using Volo.Abp.Modularity;

namespace Susalem.DC;

[DependsOn(
    typeof(DCApplicationModule),
    typeof(DCDomainTestModule)
    )]
public class DCApplicationTestModule : AbpModule
{

}
