using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.ReadModels.Record;
using Susalem.Infrastructure.Models.Record;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Susalem.Infrastructure.DbContext.RecordEntityTypeConfiguration
{
    internal class PositionRecordConfiguration:IEntityTypeConfiguration<PositionRecordEntity>
    {
        public void Configure(EntityTypeBuilder<PositionRecordEntity> builder)
        {
            builder.ToTable("PositionRecord").HasIndex(t => new { t.Id, t.PositionId });
            builder.Property(e => e.CreateTime).IsRequired().HasColumnType("timestamp without time zone");
            builder.Property(e => e.PositionFunction)
                .HasMaxLength(256)
                .HasConversion(v => v.ToString(),
                v => (PositionFunctionEnum)Enum.Parse(typeof(PositionFunctionEnum), v));
            builder.Property(e => e.AreaName).HasMaxLength(512);
            builder.Property(e => e.PositionName).HasMaxLength(512);
            builder.Property(e => e.Contents)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<ICollection<RecordContent>>(v));
        }
    }
}
