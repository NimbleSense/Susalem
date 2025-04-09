using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Susalem.Mes;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MesHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class MesConsoleApiClientModule : AbpModule
{

}
