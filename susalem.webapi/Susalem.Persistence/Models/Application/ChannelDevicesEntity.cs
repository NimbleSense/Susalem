using Susalem.Infrastructure.Models;
using Susalem.Infrastructure.Models.Application;

namespace Susalem.Persistence.Models.Application;

public class ChannelDevicesEntity : DataEntityBase<int>
{
    public virtual ChannelEnitity Channel { get; set; }

    public virtual DeviceEntity Device { get; set; }

    public int ChannelId { get; set; }

    public int DeviceId { get; set; }
}
