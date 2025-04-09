using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Susalem.DC.EntityFrameworkCore;

[ConnectionStringName(DCDbProperties.ConnectionStringName)]
public class DCDbContext : AbpDbContext<DCDbContext>, IDCDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DCDbContext(DbContextOptions<DCDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDC();
    }
}
