using Susalem.Core.Application.Interfaces.Services;
using Susalem.Messages.Enumerations;
using Susalem.Messages.Features.Channel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Interfaces;

/// <summary>
/// 通道工厂, 负责管理通道和处理相关的设备信息
/// </summary>
public interface IChannelFactory
{

    IList<ICommChannel> Channels { get; }

    Task InitializeAsync();

    /// <summary>
    /// 获取设备所属的通道
    /// </summary>
    /// <param name="deviceId"></param>
    /// <returns></returns>
    ICommChannel GetChannelByDeviceId(int deviceId);

    /// <summary>
    /// 获取指定通道
    /// </summary>
    /// <param name="channelId"></param>
    /// <returns></returns>
    ICommChannel GetChannel(int channelId);

    int GetChannelDevicesCount(int channelId);

    IList<ChannelQueryModel> GetChannelsInfo();
}

/// <summary>
/// 通信通道
/// </summary>
public interface ICommChannel
{
    /// <summary>
    /// 通道详情
    /// </summary>
    string Detail { get; }

    ChannelStatus Status { get; }

    /// <summary>
    /// 通道信息
    /// </summary>
    ChannelQueryModel Channel { get; }

    /// <summary>
    /// 通道对应的监控
    /// </summary>
    IMonitorDriver MonitorDriver { get; }
}
