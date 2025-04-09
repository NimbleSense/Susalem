using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Susalem.DC.EntityFrameworkCore;

[ConnectionStringName(DCDbProperties.ConnectionStringName)]
public interface IDCDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
