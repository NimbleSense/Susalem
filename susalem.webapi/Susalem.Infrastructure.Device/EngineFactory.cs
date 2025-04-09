using System;
using Susalem.Core.Application.Commands.Events;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Device;
using Susalem.Infrastructure.Device.Engine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Susalem.Core.Application.DTOs;
using System.Collections.Concurrent;
using Susalem.Core.Application.Models;

namespace Susalem.Infrastructure.Device;

public class EngineFactory:IEngineFactory
{
    private readonly ILogger<EngineFactory> _logger;
    private readonly MonitorSetting _monitorOptions;
    private readonly IServiceBus _serviceBus;
    private readonly IServiceProvider _serviceProvider;
    private readonly IChannelFactory _channelFactory;
    private readonly IList<IEngine> _engines = new List<IEngine>();
    private readonly IList<IAlerter> _alerts = new List<IAlerter>();


    public IReadOnlyCollection<IEngine> Engines => _engines.AsReadOnly();

    public IReadOnlyCollection<IAlerter> Alerts => _alerts.AsReadOnly();


    /// <summary>
    /// 报警灯绑定的点位和其状态
    /// </summary>
    public ConcurrentDictionary<int, Dictionary<int, AlarmLevelEnum>> AlerterPositionsStatus { get; private set; }

    public EngineFactory(ILogger<EngineFactory> logger,
        MonitorSetting monitorOptions, 
        IChannelFactory channelFactory,
        IServiceBus serviceBus,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _channelFactory = channelFactory;
        _monitorOptions = monitorOptions;
        _serviceBus = serviceBus;
        _serviceProvider = serviceProvider;

        AlerterPositionsStatus = new ConcurrentDictionary<int, Dictionary<int, AlarmLevelEnum>>();
    }

    public async Task InitializeAsync()
    {
        using var scope = _serviceProvider.CreateScope();

        var applicationDeviceService = scope.ServiceProvider.GetService<IApplicationDeviceService>();
        if (applicationDeviceService == null) throw new ArgumentNullException(nameof(applicationDeviceService));

        //初始化设备
        var deviceResult = await applicationDeviceService.GetDevicesAsync();
        if (deviceResult.Failed) return;

        _engines.Clear();

        foreach (var deviceQueryModel in deviceResult.Data)
        {
            Add(deviceQueryModel);
        }

        foreach (var alerter in _alerts)
        {
            AlerterPositionsStatus.TryAdd(alerter.Id, new Dictionary<int, AlarmLevelEnum>());
        }
    }

    private void Add(DeviceQueryModel deviceQueryModel)
    {
        var commChannel = _channelFactory.GetChannelByDeviceId(deviceQueryModel.Id);
        if (commChannel == null)
        {
            _logger.LogError($"{deviceQueryModel.Name} without channel");
            return;
        }

        var deviceEngine = Engines.FirstOrDefault(t => 
                                        t.BasicInfo.Address == deviceQueryModel.Address && 
                                        t.BasicInfo.ChannelId == commChannel.Channel.Id);

        if (deviceEngine == null)
        {
            if (deviceQueryModel.DeviceTypeName == DeviceTypeEnum.Cabinet)
            {
                deviceEngine = new Cabinet(deviceQueryModel.Address, commChannel, _logger);
                deviceEngine.EngineStatusHandler += OnEngineStatusHandler;
                _engines.Add(deviceEngine);
            }
            else
            {
                deviceEngine = new DeviceEngine(deviceQueryModel.Address, commChannel, _logger);
                deviceEngine.EngineStatusHandler += OnEngineStatusHandler;
                _engines.Add(deviceEngine);
            }
        }

        deviceEngine.BindDevice(deviceQueryModel);

        switch (deviceQueryModel.DeviceTypeName)
        {
            case DeviceTypeEnum.Alerter:
                {
                    var alerter = new Alerter(deviceEngine, _logger);
                    _alerts.Add(alerter);
                    break;
                }
        }
    }

    public IEnumerable<IEngine> GetEngines(IEnumerable<int> deviceIds)
    {
        var engines = new List<IEngine>();
        foreach (var deviceId in deviceIds)
        {
            var engine = GetEngineWithDeviceId(deviceId);
            if(engine ==null || engines.Contains(engine)) continue;

            engines.Add(engine);
        }
        return engines;
    }

    public IEngine GetEngineWithAddress(ushort address, int channelId)
    {
        return _engines.FirstOrDefault(t => t.BasicInfo.Address == address && t.BasicInfo.ChannelId == channelId);
    }

    /// <summary>
    /// 根据设备Id获取引擎
    /// </summary>
    /// <param name="deviceId"></param>
    /// <returns></returns>
    public IEngine GetEngineWithDeviceId(int deviceId)
    {
        return _engines.FirstOrDefault(t => t.BasicInfo.DeviceIds.Contains(deviceId));
    }

    public Tuple<IEngine, EngineTelemetry> GetEngineTelemetry(int deviceId, string key, PositionFunctionEnum positionFunction)
    {
        var engine = GetEngineWithDeviceId(deviceId);
        if(engine == null || !engine.Property.Online) return null;

        var telemetry = engine.Telemetries.FirstOrDefault(t => t.Key.Equals(key));

        return new Tuple<IEngine, EngineTelemetry>(engine, telemetry);
    }

    /// <summary>
    /// 更新告警的缓存状态，并更新告警灯
    /// </summary>
    /// <param name="aleterId"></param>
    /// <param name="positionId"></param>
    /// <param name="alarmLevel"></param>
    public void UpdateAlerterStatus(IAlerter alerter, int positionId, AlarmLevelEnum? alarmLevel = null)
    {
        if (AlerterPositionsStatus.TryGetValue(alerter.Id, out var positionsStatus))
        {
            //更新缓存状态
            if (alarmLevel == null)
            {
                if (positionsStatus.ContainsKey(positionId))
                    positionsStatus.Remove(positionId);
            }
            else
            {
                if (positionsStatus.ContainsKey(positionId))
                    positionsStatus[positionId] = alarmLevel.Value;
                else
                    positionsStatus.Add(positionId, alarmLevel.Value);
            }

            //触发告警灯状态
            if (positionsStatus.Count <= 0)  //不存在告警的点位
            {
                alerter.ToIdle();
                return;
            }
            var alarmCount = positionsStatus.Count(t => t.Value == AlarmLevelEnum.Alarm);
            if (alarmCount > 0)
            {
                alerter.ToAlarm();
                return;
            }
            alerter.ToWarning();
        }
    }


    /// <summary>
    /// 引擎在线状态变化事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnEngineStatusHandler(object sender, DeviceStatusEventArgs e)
    {
        await _serviceBus.Publish(new DevicesStatusChangedEvent(e.DeviceIds, e.Active ? DeviceStatus.NotStarted: DeviceStatus.Offline));
    }

    public IReadOnlyCollection<IEngine> GetEnginesByChannel(int channelId)
    {
        return _engines.Where(t=>t.BasicInfo.ChannelId == channelId).ToList();
    }
}