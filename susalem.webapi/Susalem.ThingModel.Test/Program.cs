using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Susalem.Infrastructure.Services.DbInitializerService;
using Serilog;
using Microsoft.AspNetCore.Hosting;
using Susalem.ThingModel.Test.MobudsThing;

namespace Susalem.ThingModel.Test
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    var dbInitializer = services.GetRequiredService<IDbInitializerService>();
                    logger.LogInformation($"Running database migration/seed");
                    dbInitializer.Migrate();
                    await dbInitializer.SeedAsync();

                    var thingService = services.GetRequiredService<IModbusThingService>();
                    await thingService.StartMonitor();

                }
                catch (Exception e)
                {
                    logger.LogError(e, "An error occurred while running database migration.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseWindowsService();
    }
}
