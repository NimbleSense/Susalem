using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Driver.Modbus;
using Susalem.Messages.Enumerations;
using Susalem.Messages.Features.Channel;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Susalem.Infrastructure.Device.Model;

/// <summary>
/// 通信通道
/// </summary>
public class CommChannel : ICommChannel
{
    private readonly ChannelQueryModel _channel;
    private readonly ILogger<ChannelFactory> _logger;
    private readonly IMonitorDriver _monitorDriver;

    public ChannelQueryModel Channel => _channel;

    public IMonitorDriver MonitorDriver => _monitorDriver;

    public ChannelStatus Status 
    {
        get
        {
            // 启用并且通讯状态在线
            if (Channel.Enable && _monitorDriver.IsConnected) 
                return ChannelStatus.Connected;

            return ChannelStatus.Offline;
        } 
    }

    /// <summary>
    /// 通道详情
    /// </summary>
    public string Detail { get; private set; }

    public CommChannel(ChannelQueryModel channel,  ILogger<ChannelFactory> logger) 
    {
        _channel = channel;
        _logger = logger;
        _monitorDriver = CreateDriver();
    }

    private IMonitorDriver CreateDriver()
    {
        switch (_channel.ChannelType)
        {
            case ChannelEnum.ModbusRtu:
                var serialSetting = JsonConvert.DeserializeObject<SerialSetting>(_channel.Content);
                Detail = serialSetting.PortName;
                return new ModbusRtuDriver(serialSetting, 
                    _channel.Settings,
                    _logger);
            default:
                var tcpSetting = JsonConvert.DeserializeObject<TcpSetting>(_channel.Content);
                Detail = tcpSetting.Host;
                return new ModbusTcpDriver(tcpSetting,
                    _channel.Settings,
                    _logger);
        }
    }
}
