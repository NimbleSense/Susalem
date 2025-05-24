using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using DinkToPdf;
using DinkToPdf.Contracts;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Susalem.Api
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


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Susalem api interface", Version = "v1" });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Susalem.Api.xml");
                c.IncludeXmlComments(xmlPath, true);

                c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                }); 
            });

            services.AddDatabasePersistence(Configuration);

            services.AddSharedService(Configuration);
            services.AddInfrastructureLayer(Configuration);
          
            var jwtOptions = Configuration.GetRequiredSection("JWT").Get<JwtIssuerOptions>();

            services.AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                    };
                    //options.Events = new JwtBearerEvents()
                    //{
                    //    OnMessageReceived = context =>
                    //    {
                    //        if (context.Request.Path.ToString().StartsWith("/MonitorHub"))
                    //        {
                    //            context.Token = context.Request.Query["access_token"];
                    //        }
                    //        return Task.CompletedTask;
                    //    }
                    //};
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Roles.RootManagement, policy => {
                    policy.RequireClaim(Permissions.Name, new[] { Permissions.AdavncedSetting, Permissions.DeviceAll });
                });

                options.AddPolicy(Roles.UserManagement, policy => {
                    policy.RequireClaim(Permissions.Name, new[] { Permissions.RoleAll, Permissions.UserAll });
                });

                options.AddPolicy(Roles.DeviceControl, policy => {
                    policy.RequireClaim(Permissions.Name, new[] { Permissions.PositionControl });
                });

                options.AddPolicy(Roles.DashBoard, policy => {
                    policy.RequireClaim(Permissions.Name, new[] { Permissions.AdavncedSetting, Permissions.DeviceAll, Permissions.PositionControl });
                });

                options.AddPolicy(Roles.Notification, policy => {
                    policy.RequireClaim(Permissions.Name, new[] { Permissions.NotificationAll });
                });
            });

            services.AddMediator(mediator =>
            {
                mediator.AddConsumer<AlarmNotificationHandler>();
                mediator.AddConsumer<PositionRecordsSendHandler>();

                mediator.AddConsumer<PositionStatusEventHandler>();
                mediator.AddConsumer<DevicesStatusChangedEventHandler>();

                mediator.AddApplicationConsumer();
                mediator.AddMailConsumer();
            });

            services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddSingleton<IReportService,ReportService>();
            services.AddSingleton<IPlatformService,PlatformService>();

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
                .AddJsonOptions(options=>
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
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Monitor.Web v1"));

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseInfrastructureLayer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MonitorHub>("/MonitorHub");
            });
        }
    }
}
