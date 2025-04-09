using Volo.Abp.Modularity;

namespace Susalem.Mes;

[DependsOn(
    typeof(MesApplicationModule),
    typeof(MesDomainTestModule)
    )]
public class MesApplicationTestModule : AbpModule
{

}
