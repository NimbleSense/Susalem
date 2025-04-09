using System;
using Susalem.Infrastructure.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Susalem.Infrastructure.DbContext.ApplicationEntityTypeConfiguration
{
    internal class AreaConfiguration:IEntityTypeConfiguration<AreaEntity>
    {
        public void Configure(EntityTypeBuilder<AreaEntity> builder)
        {
            builder.ToTable("Areas");
            builder.Property(e => e.Name).IsRequired().HasMaxLength(512);
            builder.HasMany(e => e.Positions)
                .WithOne(o => o.Area)
                .HasForeignKey(o => o.AreaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
