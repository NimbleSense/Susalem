using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Susalem.Common.Triggers;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Infrastructure.Models.Application;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Susalem.Infrastructure.DbContext.ApplicationEntityTypeConfiguration
{
    internal class AlarmRuleConfiguration : IEntityTypeConfiguration<AlarmRuleEntity>
    {
        public void Configure(EntityTypeBuilder<AlarmRuleEntity> builder)
        {
            builder.ToTable("AlarmRule");
            builder.Property(e => e.Name).IsRequired().HasMaxLength(256);
            builder.Property(e => e.AlarmLevel).HasConversion(v => v.ToString(),
                v => (AlarmLevelEnum)Enum.Parse(typeof(AlarmLevelEnum), v));
            builder.Property(e => e.Notification)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<NotificationSetting>(v));
            builder.Property(e => e.Rules)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, new StringEnumConverter()),
                    v => JsonConvert.DeserializeObject<ICollection<TriggerRule>>(v, new StringEnumConverter()));
            builder.Property(e => e.Settings)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<AlarmSetting>(v));
        }
    }
}
