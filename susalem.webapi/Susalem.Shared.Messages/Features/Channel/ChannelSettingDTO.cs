using Susalem.Messages.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Messages.Features.Channel;

public class ChannelSettingDTO
{
    [Required]
    public string Name { get; set; }

    [Required]
    public ChannelEnum ChannelType { get; set; }

    public string Content { get; set; }

    /// <summary>
    /// 启用/停用
    /// </summary>
    public bool Enable { get; set; }

    public CommonSetting Settings { get; set; }

    public string Description { get; set; }
}

/// <summary>
/// Tcp 配置信息
/// </summary>
/// <param name="Host"></param>
/// <param name="Port"></param>
public record TcpSetting(string Host, int Port);

/// <summary>
/// 串口 配置信息
/// </summary>
/// <param name="PortName">串口名</param>
/// <param name="BaudRate">波特率</param>
/// <param name="DataBits">数据位</param>
/// <param name="StopBits">停止位</param>
/// <param name="Parity">校验位</param>
public record SerialSetting(string PortName, int BaudRate, int DataBits, int StopBits, int Parity);

/// <summary>
/// 通用设置
/// </summary>
/// <param name="WriteTimeout">写超时(毫秒)</param>
/// <param name="readTimeout">读超时(毫秒)</param>
/// <param name="readTimeout">设备轮询间隔</param>
public record CommonSetting(int WriteTimeout, int ReadTimeout, int DeviceInterval = 300);


