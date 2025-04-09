using System.Threading.Tasks;

namespace Susalem.Data;

public interface ISusalemDbSchemaMigrator
{
    Task MigrateAsync();
}
