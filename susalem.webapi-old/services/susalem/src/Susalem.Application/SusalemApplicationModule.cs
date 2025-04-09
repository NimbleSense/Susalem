using Susalem.Identity;
using Susalem.Mes;

using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Susalem;

[DependsOn(
    typeof(SusalemDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(SusalemApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(MesApplicationModule)

    )]
public class SusalemApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<SusalemApplicationModule>();
        });
    }
}
