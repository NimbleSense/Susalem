using Susalem.Messages.Enumerations;
using Susalem.Messages.Features.Channel;
using Susalem.Persistence.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Susalem.Persistence.DbContext.ApplicationEntityTypeConfiguration;

internal class ChannelConfiguration : IEntityTypeConfiguration<ChannelEnitity>
{
    public void Configure(EntityTypeBuilder<ChannelEnitity> builder)
    {
        builder.ToTable("Channel");
        builder.Property(e => e.Name).IsRequired().HasMaxLength(256);
        builder.Property(e => e.CreateTime).IsRequired();
        builder.Property(e => e.ChannelType).HasConversion(v => v.ToString(),
            v => (ChannelEnum)Enum.Parse(typeof(ChannelEnum), v));
        builder.Property(e => e.Content).IsRequired().HasMaxLength(1024);
        builder.Property(e => e.Settings)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<CommonSetting>(v));
        builder.HasMany(e => e.ChannelDevices)
                .WithOne(d => d.Channel).HasForeignKey(t => t.ChannelId).OnDelete(DeleteBehavior.Cascade);
    }
}
