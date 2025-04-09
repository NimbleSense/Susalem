using Susalem.Core.Application.Models;
using Susalem.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Susalem.Persistence.Npgsql;

public class NpgSqlRecordDbContext: RecordDbContext
{
    private readonly TenantInfo _tenant;
    private readonly IConfiguration _configuration;

    public NpgSqlRecordDbContext()
    {
    }

    public NpgSqlRecordDbContext(TenantInfo tenant,IConfiguration configuration)
    {
        _tenant = tenant;
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connnectionString = string.Format(format: _configuration["Db:NpgSqlConnectionString"], _tenant.Year);
        optionsBuilder.UseNpgsql(connnectionString);
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;password=123456;Database=OnlineSystem");
    //}
}
