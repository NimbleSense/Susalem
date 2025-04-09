using System.Collections.Generic;
using Susalem.Core.Application.DTOs;
using Susalem.Infrastructure.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Susalem.Infrastructure.DbContext.ApplicationEntityTypeConfiguration
{
    internal class DeviceConfiguration:IEntityTypeConfiguration<DeviceEntity>
    {
        public void Configure(EntityTypeBuilder<DeviceEntity> builder)
        {
            builder.ToTable("Device");
            builder.Property(e => e.Address).IsRequired();
            builder.Property(e => e.Name).IsRequired().HasMaxLength(512);
            builder.HasOne(e => e.DeviceType)
                .WithMany(e => e.Devices)
                .HasForeignKey(o => o.DeviceTypeId);
            builder.Property(e => e.Properties)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<ICollection<DeviceFunctionProperty>>(v));
            
        }
    }
}
