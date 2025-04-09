using Susalem.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Susalem.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SusalemEntityFrameworkCoreModule),
    typeof(SusalemApplicationContractsModule)
    )]
public class SusalemDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}
