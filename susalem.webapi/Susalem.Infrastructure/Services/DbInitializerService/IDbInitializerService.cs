using System.Threading.Tasks;

namespace Susalem.Infrastructure.Services.DbInitializerService
{
    /// <summary>
    /// Initialize database
    /// </summary>
    public interface IDbInitializerService
    {
        void Migrate();

        Task SeedAsync();
    }
}
