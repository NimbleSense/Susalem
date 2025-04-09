using Susalem.Core.Application.Interfaces.Repositories;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.DbContext.ApplicationEntityTypeConfiguration;
using Susalem.Infrastructure.Models.Application;
using Susalem.Persistence.DbContext.ApplicationEntityTypeConfiguration;
using Susalem.Persistence.Models.Application;
using Microsoft.EntityFrameworkCore;

namespace Susalem.Persistence.DbContext
{
    public class ApplicationDbContext:Microsoft.EntityFrameworkCore.DbContext,IUnitOfWork
    {
        private readonly IPlatformService _platformService;

        public DbSet<ApplicationConfigurationEntity> ApplicationConfigurations { get; set; }
        
        public DbSet<DeviceTypeEntity> DeviceTypes { get; set; }

        public DbSet<AreaEntity> Areas { get; set; }
        
        public DbSet<DeviceEntity> Devices { get; set; }

        public DbSet<AlarmEntity> Alarms { get; set; }

        public DbSet<PositionEntity> Positions { get; set; }

        public DbSet<AlarmRuleEntity> AlarmSettings { get; set; }

        public DbSet<ChannelEnitity> Channels { get; set; }

        public DbSet<ChannelDevicesEntity> ChannelDevices { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(IPlatformService platformService, DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _platformService = platformService;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_platformService.DbPath}/Application.db;Pooling=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationConfigConfiguration());
            modelBuilder.ApplyConfiguration(new AreaConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new AlarmConfiguration());
            modelBuilder.ApplyConfiguration(new AlarmRuleConfiguration());
            modelBuilder.ApplyConfiguration(new ChannelConfiguration());
            modelBuilder.ApplyConfiguration(new ChannelDevicesConfiguration());
        }
    }
}
