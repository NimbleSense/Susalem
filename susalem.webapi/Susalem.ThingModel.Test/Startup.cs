using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using Susalem.Api.Handlers;
using Susalem.Api.Interfaces;
using Susalem.Api.Services;
using Susalem.Api.Utilities;
using Susalem.Core.Application;
using Susalem.Core.Application.Extensions;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.Extensions;
using Susalem.Infrastructure.Middleware;
using Susalem.Infrastructure.Options;
using Susalem.Notification.Mail;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Susalem.ThingModel.Test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();



            services.AddDatabasePersistence(Configuration);

            services.AddSharedService(Configuration);
            services.AddInfrastructureLayer(Configuration);

            var jwtOptions = Configuration.GetRequiredSection("JWT").Get<JwtIssuerOptions>();

  
            services.AddSingleton<IReportService, ReportService>();
            services.AddSingleton<IPlatformService, PlatformService>();
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddLocalization();
            services.AddRequestLocalization(options =>
            {
                var supportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("zh-CN"),
                    new CultureInfo("en-US"),
                    new CultureInfo("zh-TW")
                };
                options.DefaultRequestCulture = new RequestCulture("zh-CN", "zh-CN");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.RequestCultureProviders = new List<IRequestCultureProvider>()
                {
                    new AcceptLanguageHeaderRequestCultureProvider()
                };
            });

            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                });
            services.AddSignalR().AddJsonProtocol(configure =>
            {
                configure.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                configure.PayloadSerializerOptions.Converters.Add(new DateTimeConverter());
            });

            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<TenantInfoMiddleware>();
            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>()?
                .Value);

            app.UseDefaultFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
                {
                    {".apk","application/vnd.android.package-archive" }
                })
            });
            app.UseStaticFiles();
            app.UseWebSockets();

            app.UseCors(builder =>
                         builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .WithHeaders("X-Pagination"));
          

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHub<MonitorHub>("/MonitorHub");
            });
        }
    }
}
