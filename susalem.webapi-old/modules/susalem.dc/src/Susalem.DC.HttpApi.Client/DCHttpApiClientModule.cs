using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Susalem.DC;

[DependsOn(
    typeof(DCApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class DCHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DCApplicationContractsModule).Assembly,
            DCRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DCHttpApiClientModule>();
        });

    }
}
