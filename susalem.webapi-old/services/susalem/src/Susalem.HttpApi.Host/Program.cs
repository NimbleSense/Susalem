using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Threading.Tasks;

namespace Susalem;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
                     .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                     .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                     .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                     .Enrich.FromLogContext()
                     .WriteTo.Logger(log => log.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information).WriteTo.File($"Logs/{(DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture) + "/info.txt")}", fileSizeLimitBytes: 8388608000), LogEventLevel.Information)
                     .WriteTo.Logger(log => log.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning).WriteTo.File($"Logs/{(DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture) + "/warning.txt")}", fileSizeLimitBytes: 83886080), LogEventLevel.Warning)
                     .WriteTo.Logger(log => log.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Fatal).WriteTo.File($"Logs/{(DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture) + "/fatal.txt")}", fileSizeLimitBytes: 83886080), LogEventLevel.Fatal)
                     .WriteTo.Logger(log => log.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error).WriteTo.File($"Logs/{(DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture) + "/error.txt")}", fileSizeLimitBytes: 83886080), LogEventLevel.Error)
                     .WriteTo.Async(c => c.Console())
                     .CreateLogger();

        try
        {
            Log.Information($"Starting Susalem.HttpApi.Host.");

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                   .UseAutofac()
                   .UseSerilog();
            await builder.AddApplicationAsync<SusalemHttpApiHostModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            string type = ex.GetType().Name;
            if (type.Equals("StopTheHostException", StringComparison.Ordinal))
            {
                throw;
            }
            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}
