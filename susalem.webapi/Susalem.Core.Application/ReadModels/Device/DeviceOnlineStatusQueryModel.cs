namespace Susalem.Core.Application.ReadModels.Device;

public class DeviceOnlineStatusQueryModel:DeviceQueryModel
{
    /// <summary>
    /// 设备在线状态
    /// </summary>
    public bool Online { get; set; }
}