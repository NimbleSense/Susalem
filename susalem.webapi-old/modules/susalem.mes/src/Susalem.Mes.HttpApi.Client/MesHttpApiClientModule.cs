using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Susalem.Mes;

[DependsOn(
    typeof(MesApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class MesHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(MesApplicationContractsModule).Assembly,
            MesRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MesHttpApiClientModule>();
        });

    }
}
