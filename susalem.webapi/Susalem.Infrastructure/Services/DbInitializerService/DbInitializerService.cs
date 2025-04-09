using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Susalem.Core.Application;
using Susalem.Core.Application.Models;
using Susalem.Infrastructure.DbContext;
using Susalem.Infrastructure.Models.Application;
using Susalem.Infrastructure.Models.Identity;
using Susalem.Persistence.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Susalem.Infrastructure.Services.DbInitializerService
{
    public class DbInitializerService:IDbInitializerService
    {
        private readonly ILogger<DbInitializerService> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IdentityDbContext _identityDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public DbInitializerService(ApplicationDbContext applicationDbContext,
                IdentityDbContext identityDbContext,
                UserManager<ApplicationUser> userManager,
                IConfiguration configuration,
                ILogger<DbInitializerService> logger)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _identityDbContext = identityDbContext;
            _userManager = userManager;
            _configuration = configuration;
        }

        public void Migrate()
        {
            _applicationDbContext.Database.Migrate();
            _identityDbContext.Database.Migrate();

            _applicationDbContext.Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL");
            _identityDbContext.Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL");
        }

        public async Task SeedAsync()
        {
            await SeedUsersAsync();
            SeedConfigurations();
            SeedDeviceTypes();

            await _applicationDbContext.SaveChangesAsync();
        }

        private async Task SeedUsersAsync() 
        {
            var users = _identityDbContext.Users;
            if (!users.Any())
            {
                //add super admin account
                var user = new ApplicationUser
                {
                    UserName = Configuration.SuperAdminName,
                    IsActive = true
                };
                await _userManager.CreateAsync(user, "admin123");
                var claims = new List<Claim>()
                {
                    new Claim(Permissions.Name, Permissions.AdavncedSetting),
                    new Claim(Permissions.Name, Permissions.DeviceAll),
                    new Claim(Permissions.Name, Permissions.RoleAll),
                    new Claim(Permissions.Name, Permissions.UserAll),
                    new Claim(Permissions.Name, Permissions.PositionControl),
                    new Claim(Permissions.Name, Permissions.NotificationAll)
                };
                await _userManager.AddClaimsAsync(user, claims);

            }
        }

        private void SeedDeviceTypes()
        {
            SeedDeviceType(DbContent.Cabinet);
            SeedDeviceType(DbContent.Alerter);
        }

        private void SeedDeviceType(DeviceTypeEntity deviceTypeEntity)
        {
            var deviceItem = _applicationDbContext.DeviceTypes.FirstOrDefault(t => t.Name.Equals(deviceTypeEntity.Name));
            if (deviceItem != null) return;

            _applicationDbContext.DeviceTypes.Add(deviceTypeEntity);
        }

        private void SeedConfigurations()
        {
            SeedConfigurationItem(Configuration.CustomerNameKey, "客户公司名称", "Susalem");
            SeedConfigurationItem(Configuration.ExportExcel, "开启Excel导出", "true");
            SeedConfigurationItem(Configuration.MonitorSettingKey, "监控采集设置", JsonConvert.SerializeObject(new MonitorSetting()));
            SeedConfigurationItem(Configuration.MailSettingKey, "邮件配置", JsonConvert.SerializeObject(new MailSetting()));
            SeedConfigurationItem(Configuration.WebhookSettingKey, "Webhook配置", JsonConvert.SerializeObject(new WebhookSetting()));
        }

        private void SeedConfigurationItem(string key, string description, string value)
        {
            var configureItem = _applicationDbContext.ApplicationConfigurations.FirstOrDefault(t => t.Id.Equals(key));
            if (configureItem == null)
            {
                _applicationDbContext.ApplicationConfigurations.Add(new ApplicationConfigurationEntity
                {
                    Id = key,
                    Description = description,
                    Value = value
                });
            }
        }
    }
}
