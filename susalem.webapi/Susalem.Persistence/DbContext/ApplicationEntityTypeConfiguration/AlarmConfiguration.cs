using System;
using System.Collections.Generic;
using System.Text.Json;
using Susalem.Core.Application.DTOs.Record;
using Susalem.Core.Application.Enumerations;
using Susalem.Infrastructure.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Susalem.Infrastructure.DbContext.ApplicationEntityTypeConfiguration
{
    internal class AlarmConfiguration : IEntityTypeConfiguration<AlarmEntity>
    {
        public void Configure(EntityTypeBuilder<AlarmEntity> builder)
        {
            builder.ToTable("Alarms");
            builder.Property(e => e.ReportTime).IsRequired();
            builder.Property(e => e.Level).HasConversion(v => v.ToString(),
                v => (AlarmLevelEnum)Enum.Parse(typeof(AlarmLevelEnum), v));
            builder.Property(e => e.AlarmDetails).HasConversion(v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<ICollection<AlarmDetailDTO>>(v));
        }
    }
}
