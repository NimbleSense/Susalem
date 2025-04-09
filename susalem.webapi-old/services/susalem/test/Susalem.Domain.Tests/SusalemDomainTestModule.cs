using Susalem.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Susalem;

[DependsOn(
    typeof(SusalemEntityFrameworkCoreTestModule)
    )]
public class SusalemDomainTestModule : AbpModule
{

}
