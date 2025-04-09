using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Susalem.DC;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DCHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class DCConsoleApiClientModule : AbpModule
{

}
