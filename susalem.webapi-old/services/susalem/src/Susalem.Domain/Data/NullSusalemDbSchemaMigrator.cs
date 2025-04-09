using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Susalem.Data;

/* This is used if database provider does't define
 * ISusalemDbSchemaMigrator implementation.
 */
public class NullSusalemDbSchemaMigrator : ISusalemDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
