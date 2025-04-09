using Volo.Abp.Modularity;

namespace Susalem;

[DependsOn(
    typeof(SusalemApplicationModule),
    typeof(SusalemDomainTestModule)
    )]
public class SusalemApplicationTestModule : AbpModule
{

}
