using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Susalem.Infrastructure.DbContext
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IPlatformService _platformService;

        public IdentityDbContext()
        {
        }

        public IdentityDbContext(IPlatformService platformService, DbContextOptions<IdentityDbContext> options) : base(options)
        {
            _platformService = platformService;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_platformService.DbPath}/Identity.db;Pooling=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
