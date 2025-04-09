using Localization.Resources.AbpUi;
using Susalem.DC.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Susalem.DC;

[DependsOn(
    typeof(DCApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class DCHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DCHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DCResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
