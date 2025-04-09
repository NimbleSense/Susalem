using Localization.Resources.AbpUi;
using Susalem.Mes.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Susalem.Mes;

[DependsOn(
    typeof(MesApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class MesHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(MesHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<MesResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
