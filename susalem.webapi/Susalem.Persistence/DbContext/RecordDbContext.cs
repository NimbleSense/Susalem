using Susalem.Core.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Susalem.Infrastructure.Models.Record;
using Susalem.Infrastructure.DbContext.RecordEntityTypeConfiguration;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Models;

namespace Susalem.Infrastructure.DbContext
{
    public class RecordDbContext: Microsoft.EntityFrameworkCore.DbContext, IUnitOfWork
    {
        private readonly IPlatformService _platfromService;
        private readonly TenantInfo _tenant;

        public DbSet<PositionRecordEntity> PositionRecords { get; set; }

        public RecordDbContext()
        {
        }

        public RecordDbContext(IPlatformService platfromService,
            DbContextOptions<RecordDbContext> options,
            TenantInfo tenant) : base(options)
        {
            _platfromService = platfromService;
            _tenant = tenant;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_platfromService.DbPath}/MonitorSystem{_tenant.Year}.db;Pooling=True;");
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite($"Data Source=C:/ProgramData/SusalemServer3.0/MonitorSystem.db;Pooling=True;");
        //}
                                                                                
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PositionRecordConfiguration());
        }
    }
}
