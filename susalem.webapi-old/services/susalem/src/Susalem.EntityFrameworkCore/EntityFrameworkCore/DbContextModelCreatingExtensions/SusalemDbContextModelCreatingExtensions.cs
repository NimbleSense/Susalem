using Microsoft.EntityFrameworkCore;

using Volo.Abp;

namespace Susalem.EntityFrameworkCore.DbContextModelCreatingExtensions
{
    public static class SusalemDbContextModelCreatingExtensions
    {
        public static void ConfigureService(
         this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

        }
    }
}
