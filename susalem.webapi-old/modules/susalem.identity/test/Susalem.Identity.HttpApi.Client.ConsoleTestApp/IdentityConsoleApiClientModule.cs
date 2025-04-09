using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Susalem.Identity;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(IdentityHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class IdentityConsoleApiClientModule : AbpModule
{

}
