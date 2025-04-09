using System.Collections.Generic;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;

namespace Susalem.Core.Application.Commands.Events
{
    /// <summary>
    /// 设备在线状态事件变化
    /// </summary>
    /// <param name="DeviceIds">设备列表</param>
    /// <param name="Status">设备状态</param>
    public record DevicesStatusChangedEvent(ICollection<int> DeviceIds, DeviceStatus Status) : INotification;
}
