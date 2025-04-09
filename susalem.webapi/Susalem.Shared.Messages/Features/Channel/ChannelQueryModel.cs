using Susalem.Messages.Enumerations;

namespace Susalem.Messages.Features.Channel;

public class ChannelQueryModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ChannelEnum ChannelType { get; set; }

    public ChannelStatus Status { get; set; }

    public CommonSetting Settings { get; set; }

    public string Content { get; set; }

    public int Devices { get; set; }

    public bool Enable { get; set; }

    public string Description { get; set; }

    public DateTime CreateTime { get; set; }

    public string Title { get; set; }
}

