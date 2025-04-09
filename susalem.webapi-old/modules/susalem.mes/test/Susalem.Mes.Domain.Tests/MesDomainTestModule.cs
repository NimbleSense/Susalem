using Susalem.Mes.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Susalem.Mes;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(MesEntityFrameworkCoreTestModule)
    )]
public class MesDomainTestModule : AbpModule
{

}
