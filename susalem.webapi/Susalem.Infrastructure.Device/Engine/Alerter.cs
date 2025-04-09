using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Device;
using Susalem.Infrastructure.Device.Constants;
using Microsoft.Extensions.Logging;
using Stateless;

namespace Susalem.Infrastructure.Device.Engine
{
    internal  enum LightingState
    {
        /// <summary>
        /// 未定义
        /// </summary>
        Undefined,
        /// <summary>
        /// 准备就绪，绿灯
        /// </summary>
        Ready,
        /// <summary>
        /// 预警，黄灯
        /// </summary>
        Warning,
        /// <summary>
        /// 告警，红灯
        /// </summary>
        Alarm,
    }

    internal enum LightingCommand
    {
        /// <summary>
        /// 启动或恢复绿灯
        /// </summary>
        StartUp,
        /// <summary>
        /// 停止
        /// </summary>
        Stop,
        /// <summary>
        /// 预警
        /// </summary>
        Warning,
        /// <summary>
        /// 告警
        /// </summary>
        Alarm,
        /// <summary>
        /// 静音
        /// </summary>
        Mute,
        /// <summary>
        /// 开启声音
        /// </summary>
        UnMute
    }

    /// <summary>
    /// 报警器
    /// </summary>
    internal class Alerter : IAlerter
    {
        private readonly DeviceQueryModel _deviceModel;
        private readonly IMonitorDriver _driver;
        private readonly IEngine _engine;
        private readonly ILogger<EngineFactory> _logger;
        private bool _buzzingEnabled = true;

        private readonly StateMachine<LightingState, LightingCommand> _state;

        public ushort Address => _deviceModel.Address;
        public string Name => _deviceModel.Name;
        public int Id => _deviceModel.Id;

        public bool BuzzingEnabled
        {
            get => _buzzingEnabled;
            set
            {
                _buzzingEnabled = value;
                if (_buzzingEnabled)//打开蜂鸣声
                {
                    if (_state.CanFire(LightingCommand.UnMute))
                    {
                        _state.Fire(LightingCommand.UnMute);
                    }
                }
                else //禁用蜂鸣声
                {
                    if (_state.CanFire(LightingCommand.Mute))
                    {
                        _state.Fire(LightingCommand.Mute);
                    }
                }
            }
        }

        public bool LightingEnabled { get; private set; } = true;

        public bool Online => _engine.Property.Online;

        public Alerter(IEngine engine, ILogger<EngineFactory> logger)
        {
            _deviceModel = engine.BasicInfo.BondedDevices.FirstOrDefault(t => t.DeviceTypeName == DeviceTypeEnum.Alerter);
            _driver = engine.CommChannel.MonitorDriver;
            _engine = engine;

            _logger = logger;
            _state = new StateMachine<LightingState, LightingCommand>(LightingState.Undefined);
            _state.OnTransitioned(OnTransition);

            _state.Configure(LightingState.Undefined)
                .PermitIf(LightingCommand.StartUp, LightingState.Ready, () => LightingEnabled && Online)
                .OnEntry(() =>
                {
                    ExecuteCommand(false, false, false, false);
                });

            _state.Configure(LightingState.Ready)
                .PermitIf(LightingCommand.Alarm, LightingState.Alarm, () => Online)
                .PermitIf(LightingCommand.Warning, LightingState.Warning, () => Online)
                .PermitIf(LightingCommand.Stop, LightingState.Undefined, () => Online)
                .OnEntry(() =>
                {
                    ExecuteCommand(true, false, false, false);
                });

            _state.Configure(LightingState.Warning)
               .InternalTransition(LightingCommand.Mute, () =>
               {
                   if (Online) ExecuteCommand(false, true, false, false);
               })
               .InternalTransition(LightingCommand.UnMute, () =>
               {
                   if (Online) ExecuteCommand(false, true, false, true);
               })
               .PermitIf(LightingCommand.StartUp, LightingState.Ready, () => Online)
               .PermitIf(LightingCommand.Alarm, LightingState.Alarm, () => Online)
               .PermitIf(LightingCommand.Stop, LightingState.Undefined, () => Online)
               .OnEntry(() =>
               {
                   ExecuteCommand(false, true, false, BuzzingEnabled);
               });

            _state.Configure(LightingState.Alarm)
               .InternalTransition(LightingCommand.Mute, () =>
               {
                   if (Online) ExecuteCommand(false, false, true, false);
               })
               .InternalTransition(LightingCommand.UnMute, () =>
               {
                   if (Online) ExecuteCommand(false, false, true, true);
               })
               .PermitIf(LightingCommand.StartUp, LightingState.Ready, () => Online)
               .PermitIf(LightingCommand.Warning, LightingState.Warning, () => Online)
               .PermitIf(LightingCommand.Stop, LightingState.Undefined, () => Online)
               .OnEntry(() =>
               {
                   ExecuteCommand(false, false, true, BuzzingEnabled);
               });
        }

        public void ToAlarm()
        {
            if (_state.CanFire(LightingCommand.Alarm))
            {
                _state.Fire(LightingCommand.Alarm);
            }
        }

        public void ToWarning()
        {
            if (_state.CanFire(LightingCommand.Warning))
            {
                _state.Fire(LightingCommand.Warning);
            }
        }

        public void ToIdle()
        {
            if (_state.CanFire(LightingCommand.StartUp))
            {
                _state.Fire(LightingCommand.StartUp);
            }
        }

        public void ToUndefined()
        {
            if (_state.CanFire(LightingCommand.Stop))
            {
                _state.Fire(LightingCommand.Stop);
            }
        }

        private bool ExecuteCommand(bool green, bool yellow, bool red, bool buzzing)
        {
            var engineCommands = new List<EngineCommand>();
            _deviceModel.Properties.ForEach(t =>
            {
                if (t.Reg != null)
                {
                    var engineCommand = t.Key switch
                    {
                        DeviceConstants.LightGreen => new EngineCommand((ushort)t.Reg, green ? (ushort)1 : (ushort)0),
                        DeviceConstants.LightYellow => new EngineCommand((ushort)t.Reg, yellow ? (ushort)1 : (ushort)0),
                        DeviceConstants.LightRed => new EngineCommand((ushort)t.Reg, red ? (ushort)1 : (ushort)0),
                        _ => new EngineCommand((ushort)t.Reg, buzzing ? (ushort)1 : (ushort)0),
                    };
                    engineCommands.Add(engineCommand);
                }
            });
            return _driver.ExecuteCoil(_deviceModel.Address, engineCommands);
        }

        public void SetLightingStatus(bool isLightingEnabled, bool isMonitoring)
        {
            LightingEnabled = isLightingEnabled;
            if (isMonitoring)
            {
                if (isLightingEnabled)
                {
                    ToIdle();
                }
                else
                {
                    ToUndefined();
                }
            }
            else
            {
                ToUndefined();
            }
        }

        private void OnTransition(StateMachine<LightingState, LightingCommand>.Transition transition)
        {
            _logger.LogInformation($"Alerter: {Address} Transitioned from {transition.Source} to " +
                $"{transition.Destination} via {transition.Trigger}.");
        }
    }
}
