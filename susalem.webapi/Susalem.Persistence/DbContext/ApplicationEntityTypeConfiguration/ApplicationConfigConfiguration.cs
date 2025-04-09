using Susalem.Infrastructure.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Susalem.Infrastructure.DbContext.ApplicationEntityTypeConfiguration
{
    internal class ApplicationConfigConfiguration : IEntityTypeConfiguration<ApplicationConfigurationEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationConfigurationEntity> builder)
        {
            builder.ToTable("Configurations");
            builder.Property(e => e.Id).HasMaxLength(256);
            builder.Property(e => e.Value).HasMaxLength(256);
            builder.Property(e => e.Description).HasMaxLength(1024);

        }
    }
}
