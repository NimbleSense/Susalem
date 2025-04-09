using Serilog;
using Serilog.Events;

namespace SusalemAbp.webapi
{
    public class Program
    {
        public async static Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
         //   .MinimumLevel.Debug()
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
                Log.Information("Starting Susalem.HttpApi.Host.");
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
                return 0;
            }
            catch (Exception ex)
            {
                if (ex is HostAbortedException)
                {
                    throw;
                }

                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}