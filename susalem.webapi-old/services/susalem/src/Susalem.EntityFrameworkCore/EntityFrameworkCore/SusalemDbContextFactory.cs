using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


using Susalem.Mes.EntityFrameworkCore;

using System;
using System.IO;

namespace Susalem.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class SusalemDbContextFactory : IDesignTimeDbContextFactory<SusalemDbContext>
{
    public SusalemDbContext CreateDbContext(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        SusalemEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();
       // 更改为pgsql
        var builder = new DbContextOptionsBuilder<SusalemDbContext>()
           .UseNpgsql(configuration.GetConnectionString("Default"));
        //var builder = new DbContextOptionsBuilder<SusalemDbContext>()
        //    .UseMySql(configuration.GetConnectionString("Default"), MySqlServerVersion.LatestSupportedServerVersion);

        return new SusalemDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Susalem.HttpApi.Host/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
