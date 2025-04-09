using System;
using System.Collections.Generic;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Infrastructure.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Susalem.Infrastructure.DbContext.ApplicationEntityTypeConfiguration
{
    internal class DeviceTypeConfiguration:IEntityTypeConfiguration<DeviceTypeEntity>
    {
        public void Configure(EntityTypeBuilder<DeviceTypeEntity> builder)
        {
            builder.ToTable("DeviceType");
            builder.Property(e => e.Name).HasConversion(v => v.ToString(),
                v => (DeviceTypeEnum)Enum.Parse(typeof(DeviceTypeEnum), v));
            builder.Property(e => e.Description).HasMaxLength(1024);
            builder.Property(e => e.Properties)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<ICollection<DeviceTypeProperty>>(v));
        }
    }
}
