using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.Device.Model;
using Susalem.Messages.Features.Channel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Susalem.Infrastructure.Device;

/// <summary>
/// 通道工厂, 负责管理通道和处理相关的设备信息
/// </summary>
public class ChannelFactory : IChannelFactory
{
    private readonly ILogger<ChannelFactory> _logger;
    private readonly IServiceProvider _serviceProvider;
    private Dictionary<int, List<int>> _channelDevices;

    public IList<ICommChannel> Channels { get; } = new List<ICommChannel>();

    public ChannelFactory(ILogger<ChannelFactory> logger, 
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task InitializeAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var channelService = scope.ServiceProvider.GetService<IChannelService>();
        var result = await channelService.GetChannelsAsync();
        if (!result.Succeeded) return;

        foreach ( var channel in result.Data)
        {
            if (!channel.Enable)
            {
                _logger.LogWarning($"Channel: {channel.Name} is disabled");
            }

            Channels.Add(new CommChannel(channel, _logger));
        }

        _channelDevices = await channelService.GetChannelDevicesAsync();
    }

    public ICommChannel GetChannelByDeviceId(int deviceId)
    {
        foreach (var channelDevice in _channelDevices)
        {
            if (channelDevice.Value.Contains(deviceId))
            {
                return Channels.FirstOrDefault(t=>t.Channel.Id == channelDevice.Key);
            }
        }
        return null;
    }

    public ICommChannel GetChannel(int channelId)
    {
        return Channels.FirstOrDefault(t=>t.Channel.Id ==channelId);
    }

    public IList<ChannelQueryModel> GetChannelsInfo()
    {
        var channelQueryModels = new List<ChannelQueryModel>();
        foreach(var comChannel in Channels)
        {
            comChannel.Channel.Devices = GetChannelDevicesCount(comChannel.Channel.Id);
            comChannel.Channel.Status = comChannel.Status;
            channelQueryModels.Add(comChannel.Channel);
        }
        return channelQueryModels;
    }

    public int GetChannelDevicesCount(int channelId)
    {
        if(_channelDevices.TryGetValue(channelId, out var devices))
        {
            return devices.Count;
        }
        return 0;
    }
}
