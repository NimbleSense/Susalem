using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.Interfaces.Services;

namespace Susalem.Core.Application.Interfaces;

/// <summary>
/// 设备引擎工厂
/// </summary>
public interface IEngineFactory
{
    /// <summary>
    /// 设备引擎
    /// </summary>
    IReadOnlyCollection<IEngine> Engines { get; }

    /// <summary>
    /// 所有的报警器
    /// </summary>
    IReadOnlyCollection<IAlerter> Alerts { get; }


    ConcurrentDictionary<int, Dictionary<int, AlarmLevelEnum>> AlerterPositionsStatus { get; }

    /// <summary>
    /// 加载引擎数据
    /// </summary>
    /// <returns></returns>
    Task InitializeAsync();

    /// <summary>
    /// 根据通信地址获取引擎
    /// </summary>
    /// <param name="address">设备地址</param>
    /// <param name="channelId">通道id</param>
    /// <returns></returns>
    IEngine GetEngineWithAddress(ushort address, int channelId);

    /// <summary>
    /// 根据设备Id获取引擎
    /// </summary>
    /// <param name="deviceId"></param>
    /// <returns></returns>
    IEngine GetEngineWithDeviceId(int deviceId);

    Tuple<IEngine,EngineTelemetry> GetEngineTelemetry(int deviceId, string key, PositionFunctionEnum positionFunction);

    /// <summary>
    /// 根据设备ID列表获取引擎列表
    /// </summary>
    /// <param name="deviceIds"></param>
    /// <returns></returns>
    IEnumerable<IEngine> GetEngines(IEnumerable<int> deviceIds);

    /// <summary>
    /// 获取指定通道下的所有设备引擎
    /// </summary>
    IReadOnlyCollection<IEngine> GetEnginesByChannel(int channelId);

    /// <summary>
    /// 更新告警的缓存状态，并更新告警灯
    /// </summary>
    /// <param name="aleterId"></param>
    /// <param name="positionId"></param>
    /// <param name="alarmLevel"></param>
    void UpdateAlerterStatus(IAlerter alerter, int positionId, AlarmLevelEnum? alarmLevel = null);
}