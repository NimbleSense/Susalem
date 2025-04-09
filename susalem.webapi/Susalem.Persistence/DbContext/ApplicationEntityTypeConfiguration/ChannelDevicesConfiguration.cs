using Susalem.Persistence.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Susalem.Persistence.DbContext.ApplicationEntityTypeConfiguration;

internal class ChannelDevicesConfiguration : IEntityTypeConfiguration<ChannelDevicesEntity>
{
    public void Configure(EntityTypeBuilder<ChannelDevicesEntity> builder)
    {
        builder.ToTable("ChannelDevices");
        builder.HasOne(e => e.Channel);
        builder.HasOne(e => e.Device);
    }
}
