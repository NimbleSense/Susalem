namespace Susalem.Messages.Enumerations;

/// <summary>
/// 设备连接通道
/// </summary>
public enum ChannelEnum
{
    /// <summary>
    /// Modbus RTU
    /// </summary>
    ModbusRtu,

    ModbusTcp
}

/// <summary>
/// 通道连接状态
/// </summary>
public enum ChannelStatus
{
    /// <summary>
    /// 离线
    /// </summary>
    Offline,

    /// <summary>
    ///  已连接
    /// </summary>
    Connected,

    /// <summary>
    /// 故障
    /// </summary>
    Fault
}
