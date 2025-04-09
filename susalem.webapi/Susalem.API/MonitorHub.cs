using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Alarm;
using Susalem.Core.Application.ReadModels.Record;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Susalem.Api
{
    /// <summary>
    /// Receive message from stream, and send to front end.
    /// </summary>
    public class MonitorHub : Hub<IMessageNotification>
    {
        private readonly ILogger<MonitorHub> _logger;
        private readonly IMonitorLoop _monitorLoop;
        private readonly IPositionFactory _positionFactory;

        public MonitorHub(ILogger<MonitorHub> logger, 
            IMonitorLoop monitorLoop,
            IPositionFactory positionFactory)
        {
            _logger = logger;
            _monitorLoop = monitorLoop;
            _positionFactory = positionFactory;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            await Clients.Client(Context.ConnectionId).MonitoringStatusChanged(_monitorLoop.IsMonitoring);
            await Clients.Client(Context.ConnectionId).AlerterBuzzingChanged(_monitorLoop.IsEnableBuzzing);
            await Clients.Client(Context.ConnectionId).AlerterLightingChanged(_monitorLoop.IsEnableLighting);

            // 通知不在线的设备列表
            await Clients.Client(Context.ConnectionId).DevicesStatusChanged(_monitorLoop.OfflineDevices, DeviceStatus.Offline);

            // 通知监控中的点位
            await Clients.Client(Context.ConnectionId).MonitoringPositions(_monitorLoop.MonitoringPositionIds);

            _logger.LogInformation($"Client connected: {Context.ConnectionId}");
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation($"Client disconnected: {Context.ConnectionId} {exception}");
            return base.OnDisconnectedAsync(exception);
        }      
    }

    /// <summary>
    /// 消息事件通知
    /// </summary>
    public interface IMessageNotification
    {
        /// <summary>
        /// Report Alarm event
        /// </summary>
        /// <param name="alarmQueryModel"></param>
        /// <returns></returns>
        Task AlarmReported(AlarmQueryModel alarmQueryModel);

        /// <summary>
        /// Update position changed data
        /// </summary>
        /// <param name="positionId">Position ID</param>
        /// <param name="function">Position function</param>
        /// <param name="recordContents">Changed record contents</param>
        /// <returns></returns>
        Task PositionRecordReported(int positionId, string function, ICollection<RecordContent> recordContents, bool door1, bool door2, bool door3, bool door4);

        /// <summary>
        /// Update position status
        /// </summary>
        /// <param name="status">position status</param>
        /// <returns></returns>
        Task PositionStatusChanged(PositionMonitoringStatus status);

        /// <summary>
        /// 监控状态变化
        /// </summary>
        /// <param name="isMonitoring">是否在监控</param>
        /// <returns></returns>
        Task MonitoringStatusChanged(bool isMonitoring);

        /// <summary>
        /// 被取消监控的点位
        /// </summary>
        /// <param name="positionIds">点位列表</param>
        /// <returns></returns>
        Task MonitorCancelledPositions(ICollection<int> positionIds);

        /// <summary>
        /// 正在监控中的点位
        /// </summary>
        /// <param name="positionIds"></param>
        /// <returns></returns>
        Task MonitoringPositions(ICollection<int> positionIds);

        /// <summary>
        /// 设备状态变化
        /// </summary>
        /// <param name="deviceIds">设备列表</param>
        /// <param name="deviceStatus">设备状态</param>
        /// <returns></returns>
        Task DevicesStatusChanged(ICollection<int> deviceIds, DeviceStatus deviceStatus);

        /// <summary>
        /// 报警器的报警灯可用状态变化
        /// </summary>
        /// <param name="enable">是否启用</param>
        /// <returns></returns>
        Task AlerterLightingChanged(bool enable);

        /// <summary>
        /// 报警器的蜂鸣声可用状态变化
        /// </summary>
        /// <param name="enable">是否启用</param>
        /// <returns></returns>
        Task AlerterBuzzingChanged(bool enable);

    }
}