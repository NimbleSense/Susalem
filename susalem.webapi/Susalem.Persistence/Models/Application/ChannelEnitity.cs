using Susalem.Infrastructure.Models;
using Susalem.Infrastructure.Models.Application;
using Susalem.Messages.Enumerations;
using Susalem.Messages.Features.Channel;
using System;
using System.Collections.Generic;

namespace Susalem.Persistence.Models.Application;

/// <summary>
/// 通信通道
/// </summary>
public class ChannelEnitity: DataEntityBase<int>
{
    public string Name { get; set; }

    public ChannelEnum ChannelType {get; set;}

    public string Content { get; set; }

    public bool Enable { get; set; }

    public string Description { get; set; }

    public CommonSetting Settings { get; set; }

    public DateTime CreateTime { get; set; }

    public virtual ICollection<ChannelDevicesEntity> ChannelDevices { get; set; } = new List<ChannelDevicesEntity>();
}
