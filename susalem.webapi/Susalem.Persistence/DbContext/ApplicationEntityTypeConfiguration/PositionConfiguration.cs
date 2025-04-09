using System;
using System.Collections.Generic;
using Susalem.Core.Application.DTOs;
using Susalem.Infrastructure.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Susalem.Infrastructure.DbContext.ApplicationEntityTypeConfiguration
{
    internal class PositionConfiguration : IEntityTypeConfiguration<PositionEntity>
    {
        public void Configure(EntityTypeBuilder<PositionEntity> builder)
        {
            builder.ToTable("Position");
            builder.Property(e => e.Name).IsRequired().HasMaxLength(512);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(1024);
            builder.HasMany(e => e.Alarms)
                .WithOne(d => d.Position).HasForeignKey(t => t.PositionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(e => e.Rules)
                .WithOne(d => d.Position).HasForeignKey(t => t.PositionId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(e => e.Functions)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<IList<PositionFunctionProperty>>(v));
            builder.Property(e => e.BoundedAlerter)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<ICollection<int>>(v));
        }
    }
}
