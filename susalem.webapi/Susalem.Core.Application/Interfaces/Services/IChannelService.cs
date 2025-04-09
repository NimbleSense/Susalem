using Susalem.Common.Results;
using Susalem.Core.Application.ReadModels.Device;
using Susalem.Messages.Features.Channel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Interfaces.Services;

/// <summary>
/// 通讯通道
/// </summary>
public interface IChannelService
{
    Task<Result<ChannelQueryModel>> CreateChannelAsync(ChannelSettingDTO channelDto);

    Task<Result> EditChannelAsync(int channelId, ChannelSettingDTO dto);

    Task<Result> DeleteChannelAsync(int id);

    Task<Result<List<ChannelQueryModel>>> GetChannelsAsync();

    Task<Result<ChannelQueryModel>> GetChannelAsync(int id);

    /// <summary>
    /// 将所有的设备添加到当前通道下
    /// </summary>
    /// <param name="id">通道Id</param>
    /// <returns></returns>
    Task<Result> AddDevicesToChannelAsync(int id);

    /// <summary>
    /// 将所有的设备从当前通道移除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result> RemoveDevicesFromChannelAsync(int id);

    /// <summary>
    /// 获取通道和对应的设备
    /// </summary>
    /// <returns>key: 通道ID value: 设备ID列表</returns>
    Task<Dictionary<int, List<int>>> GetChannelDevicesAsync();

    /// <summary>
    /// 
    /// 获取通道下面的设备</summary>
    /// <param name="id">通道ID</param>
    /// <returns></returns>
    Task<Result<IList<DeviceOnlineStatusQueryModel>>> GetDevicesAsync(int id);
}
