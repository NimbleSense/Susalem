using System;
using System.Collections.Generic;
using System.Linq;
using Susalem.Core.Application.Commands.Events;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Models;
using Susalem.Core.Application.ReadModels.Record;
using Susalem.Infrastructure.Device.Engine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Susalem.Infrastructure.Device;

public class MonitorLoop : IMonitorLoop
{
    private readonly ILogger<MonitorLoop> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly MonitorSetting _monitorOptions;
    private readonly IEngineFactory _engineFactory;
    private readonly IPositionFactory _positionFactory;
    private readonly IConfiguration _configuration;
    private readonly IServiceBus _serviceBus;
    private readonly System.Timers.Timer _monitorPositionTimer;

    /// <summary>
    /// 点位数据触发刷新次数
    /// </summary>
    private int _triggerCount; 

    public bool IsMonitoring { get; private set; }

    /// <summary>
    /// 蜂鸣声
    /// </summary>
    public bool IsEnableBuzzing { get; private set; }

    /// <summary>
    /// 报警灯
    /// </summary>
    public bool IsEnableLighting { get; private set; }


    /// <summary>
    /// 监控中的点位ID
    /// </summary>
    public ICollection<int> MonitoringPositionIds => _positionFactory.MonitoringPositions.Keys;

    /// <summary>
    /// 离线的设备列表
    /// </summary>
    public ICollection<int> OfflineDevices
    {
        get
        {
            return _engineFactory.Engines.Where(t => !t.Property.Online)
                .SelectMany(t=>t.BasicInfo.DeviceIds)
                .Distinct().ToList();
        }
    }

    public MonitorLoop(ILogger<MonitorLoop> logger,
        IServiceProvider serviceProvider,
        MonitorSetting monitorOptions,
        IEngineFactory engineFactory,
        IPositionFactory positionFactory,
        IConfiguration configuration,
        IServiceBus serviceBus)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _monitorOptions = monitorOptions;
        _engineFactory = engineFactory;
        _positionFactory = positionFactory;
        _configuration = configuration;
        _serviceBus = serviceBus;

        IsEnableLighting = true;
        IsEnableBuzzing = true;

        _monitorPositionTimer = new System.Timers.Timer(TimeSpan.FromSeconds(_monitorOptions.PositionTimer)) { AutoReset = true, Enabled = false };
        _monitorPositionTimer.Elapsed += OnMonitorPositionTimerElapsed;
    }

    /// <summary>
    /// 设置所有报警器的蜂鸣声
    /// </summary>
    /// <param name="isEnableBuzzing"></param>
    /// <returns></returns>
    public void SetAlerterBuzzing(bool isEnableBuzzing)
    {
        IsEnableBuzzing = isEnableBuzzing;
        foreach (var alerter in _engineFactory.Alerts)
        {
            alerter.BuzzingEnabled = isEnableBuzzing;
        }
    }

    /// <summary>
    /// 设置所有报警灯的状态
    /// </summary>
    /// <param name="isEnableLighting">是否启用报警灯</param>
    public void SetAlerterLighting(bool isEnableLighting)
    {
        IsEnableLighting = isEnableLighting;
        foreach (var alerter in _engineFactory.Alerts)
        {
            alerter.SetLightingStatus(isEnableLighting, IsMonitoring);
        }
    }

    private async void OnMonitorPositionTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        try
        {           
            _triggerCount++;
            _logger.LogInformation($"Trigger positions loop: {_triggerCount}"); 

            foreach (var monitorPosition in _positionFactory.MonitoringPositions)
            {
                if (!IsMonitoring) break;

                var position = monitorPosition.Value;

                _logger.LogInformation($"Position: {monitorPosition.Value.PositionModel.Name}");
                var positionRecords = GetPositionRecords(position);
                if (positionRecords.Count <= 0)
                {
                    _logger.LogWarning($"{position.PositionModel.Name} record content is empty");
                    continue;
                }
                var doorStatus = GetDoorStatus(position);

                //点位数据创建
                await _serviceBus.Send(new CreatePositionRecordCommand(monitorPosition.Key, 
                    positionRecords,
                    doorStatus.Item1,
                    doorStatus.Item2,
                    doorStatus.Item3,
                    doorStatus.Item4
                    ));

                //更新最新的数据记录
                position.LatestPositionRecords = positionRecords;

                //触发次数达到保存频率
                if (_triggerCount >= _monitorOptions.PositionSaveFrequency)
                {
                    await _serviceBus.Send(new SavePositionRecordCommand(monitorPosition.Key, positionRecords));
                }

            }

            if (_triggerCount >= _monitorOptions.PositionSaveFrequency)
            {
                _triggerCount = 0;
            }
            _logger.LogInformation("End positions loop");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Loop positions exception:{ex}");
        }
    }

    /// <summary>
    /// Start monitor devices
    /// </summary>
    public void StartMonitorLoop()
    {
        if (IsMonitoring)
        {
            _logger.LogInformation("MonitorAsync Loop is monitoring");
            return;
        }
        _logger.LogInformation("MonitorAsync Loop is starting");
            
        IsMonitoring = true;

        _monitorPositionTimer.Start();

        _logger.LogInformation("Initialize all alerter");
        foreach (var alerter in _engineFactory.Alerts)
        {
            alerter.ToIdle();
        }

    }

    public void StopMonitorLoop()
    {
        if (!IsMonitoring)
        {
            return;
        }
        _logger.LogInformation("MonitorAsync Loop is stopping");

        foreach (var alerter in _engineFactory.Alerts)
        {
            alerter.ToUndefined();
        }

        IsMonitoring = false;
        _monitorPositionTimer.Stop();
    }

    private IList<RecordRequestDTO> GetPositionRecords(MonitorContext position)
    {
        var positionRecords = new List<RecordRequestDTO>();
        //遍历点位功能
        foreach (var positionFunction in position.PositionModel.Functions)
        {
            var recordRequest = new RecordRequestDTO
            {
                AreaName = position.PositionModel.AreaName,
                PositionId = position.PositionModel.Id,
                PositionName = position.PositionModel.Name,
                PositionFunction = positionFunction.Key,
                CreateTime = DateTime.Now
            };
            //获取点位绑定设备的结果
            foreach (var positionFunctionDevice in positionFunction.Devices)
            {
                var engineDetail = _engineFactory.GetEngineTelemetry(positionFunctionDevice.Id, positionFunctionDevice.Key, positionFunction.Key);
                if (engineDetail != null)
                {
                    recordRequest.Contents.Add(new RecordContent(engineDetail.Item2.Key,
                        engineDetail.Item2.Value,
                        engineDetail.Item1.BasicInfo.Address,
                        engineDetail.Item1.CommChannel.Detail));
                }
            }

            if (recordRequest.Contents.Count <= 0)
            {
                _logger.LogWarning($"{position.PositionModel.Name} {positionFunction.Key} record content is 0");
            }
            else
            {
                positionRecords.Add(recordRequest);
            }
            position.GatherTime = DateTime.Now;
        }

        return positionRecords;
    }


    private Tuple<bool, bool, bool, bool> GetDoorStatus(MonitorContext position)
    {
        var positionFunction = position.PositionModel.Functions.FirstOrDefault();
        if (positionFunction == null) return new Tuple<bool, bool, bool, bool>(false, false, false, false);

        var deviceFunction = positionFunction.Devices.FirstOrDefault();
        if (deviceFunction == null) return new Tuple<bool, bool, bool, bool>(false, false, false, false);

        var engine = _engineFactory.GetEngineWithDeviceId(deviceFunction.Id);
        if (engine == null) return new Tuple<bool, bool, bool, bool>(false, false, false, false);

        var cabinet = (Cabinet)engine;
        return new Tuple<bool, bool, bool, bool>(cabinet.Doors[0].Open,
            cabinet.Doors[1].Open,
            cabinet.Doors[2].Open,
            cabinet.Doors[3].Open);

    }
}