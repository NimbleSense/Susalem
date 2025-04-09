using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Susalem.Localization;
using Susalem.Mes;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.Localization;
using Volo.Abp.PermissionManagement.HttpApi;

namespace Susalem;

[DependsOn(
   typeof(SusalemApplicationContractsModule),
    typeof(AbpAccountHttpApiModule),
    typeof(Identity.AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(MesHttpApiModule)
    )]
public class SusalemHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(AbpOpenIddictResource), typeof(SusalemHttpApiModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SusalemHttpApiModule).Assembly);

        });
    }
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SusalemResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }

}
