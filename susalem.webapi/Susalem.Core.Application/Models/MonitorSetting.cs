namespace Susalem.Core.Application.Models;

/// <summary>
/// 点位监控参数设置
/// </summary>
public class MonitorSetting
{
    /// <summary>
    /// 点位触发定时间隔，单位(s)
    /// </summary>
    public int PositionTimer { get; set; } = 5;

    /// <summary>
    /// 保存频率(触发指定次数后，保存数据) (如果开启暂停设备，触发保存后，会恢复运行设备)
    /// </summary>
    public int PositionSaveFrequency { get; set; } = 12;
}
