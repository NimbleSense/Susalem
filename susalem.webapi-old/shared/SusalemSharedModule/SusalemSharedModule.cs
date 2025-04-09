using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace SusalemShared;

[DependsOn(typeof(AbpSwashbuckleModule))]
public class SusalemSharedModule : AbpModule { }