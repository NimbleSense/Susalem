using System;
using System.Collections.Generic;
using System.Linq;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Device;
using Microsoft.Extensions.Logging;

namespace Susalem.Infrastructure.Device.Engine
{
    public class DeviceEngine : IEngine
    {
        private readonly ICommChannel _commChannel;
        private readonly IMonitorDriver _monitorDriver;
        private readonly ILogger<EngineFactory> _logger;

        protected IMonitorDriver Driver => _monitorDriver;

        public EngineInfo BasicInfo { get; }
        public EngineProperty Property { get; }

        public event EventHandler<DeviceStatusEventArgs> EngineStatusHandler;

        public IList<EngineTelemetry> Telemetries { get; }

        public ICommChannel CommChannel => _commChannel;

        public DeviceEngine(ushort address, ICommChannel commChannel, ILogger<EngineFactory> logger)
        {
            _commChannel = commChannel;
            _monitorDriver = commChannel.MonitorDriver;
            _logger = logger;

            BasicInfo = new EngineInfo( address, commChannel.Channel.Id);

            Property = new EngineProperty(commChannel);
            Property.ActiveChanged += OnActiveChanged;
            
            Telemetries = new List<EngineTelemetry>();
        }

        private void OnActiveChanged(object sender, bool active)
        {
            if (active)
                _logger.LogInformation($"Online name: {string.Join(",", BasicInfo.Names)}, address: {BasicInfo.Address}, {Property} ");
            else
                _logger.LogWarning($"Offline name: {string.Join(",", BasicInfo.Names)}, address: {BasicInfo.Address}, {Property} ");

            EngineStatusHandler?.Invoke(this,new DeviceStatusEventArgs(BasicInfo.BondedDevices.Select(t=>t.Id).ToList(), active));
        }

        /// <summary>
        /// 绑定设备
        /// </summary>
        /// <param name="deviceQueryModel"></param>
        public virtual void BindDevice(DeviceQueryModel deviceQueryModel)
        {
            if (deviceQueryModel == null) return;

            if (BasicInfo.BondedDevices.FirstOrDefault(t => t.Id == deviceQueryModel.Id) != null) return;

            BasicInfo.BondedDevices.Add(deviceQueryModel);

            deviceQueryModel.Properties.ForEach(t =>
            {
                if (t.Reg != null)
                {
                    Telemetries.Add(new EngineTelemetry(t.Key)
                    {
                        Reg = (byte)t.Reg,
                        Length = t.Length,
                        Factor= t.Factor,
                        Expression = t.Formula
                    });
                }
            });
        }
      
        public virtual bool UpdateTelemetries()
        {
            if (Telemetries.Count <= 0) return false;

            if (!Property.EnableComm) return false;

            bool result;
            if(BasicInfo.BondedDevices.FirstOrDefault(t=>t.DeviceTypeName == Core.Application.Enumerations.DeviceTypeEnum.Alerter) != null)
            {
                result = _monitorDriver.ReadCoil(BasicInfo.Address, Telemetries);
            }
            else
            {
                result = _monitorDriver.Read(BasicInfo.Address, Telemetries);
            }
            Property.Update(result);

            return result;
        }

        /// <summary>
        /// 启动引擎，启动能控制的设备
        /// </summary>
        public void StartUp()
        {
            if (!Property.EnableComm) return;
        }

        /// <summary>
        /// 关闭引擎，停止能控制的设备
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Stop()
        {
            if (!Property.EnableComm) return;
        }

        public void Resume()
        {
            if (!Property.EnableComm) return;
        }
    }
}
