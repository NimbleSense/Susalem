using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Susalem.Core.Application.Localize;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.Utilities;
using Susalem.Shared.Messages.Features.System;
using System.Collections.Generic;
using Susalem.Core.Application;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Commands.Monitor;
using Microsoft.AspNetCore.SignalR;

namespace Susalem.Api.Controllers
{
    /// <summary>
    /// Action management
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController: ControllerBase
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        private readonly IPlatformService _platformService;
        private readonly IServiceBus _serviceBus;
        private readonly IMonitorLoop _monitorLoop;
        private readonly IHubContext<MonitorHub, IMessageNotification> _hubContext;

        public ActionController(
            IStringLocalizer<Resource> stringLocalizer,
            IPlatformService platformService,
            IServiceBus serviceBus,
            IMonitorLoop monitorLoop,
            IHubContext<MonitorHub, IMessageNotification> hubContext)
        {
            _stringLocalizer = stringLocalizer;
            _platformService = platformService;
            _serviceBus = serviceBus;
            _monitorLoop = monitorLoop;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Restart service
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("restart")]
        public async Task<IActionResult> RestartService()
        {
            _platformService.Restart();
            return Ok();
        }

        /// <summary>
        /// 获取所有的日志文件
        /// </summary>
        /// <returns></returns>
        [HttpGet("logs")]
        public async Task<IEnumerable<SystemLogQueryModel>> GetLogs()
        {
            return FolderHelper.GetLogs(_platformService.LogsPath);
        }

        /// <summary>
        /// 下载指定文件                 
        /// </summary>
        /// <returns></returns>
        [HttpGet("log/{name}/download")]
        public async Task<string> DownloadLog(string name)
        {
            // 20240920 anders 
            //return FolderHelper.CompressFile(_platformService.LogsPath, name, _platformService.DownloadTempFolder);
            return "666";
        }

        /// <summary>
        /// start monitor with positions
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Roles.DeviceControl)]
        [HttpPost]
        [Route("monitor/start")]
        public async Task<IActionResult> StartMonitor(ICollection<int> positionIds)
        {
            await _serviceBus.Send(new StartMonitorCommand(positionIds));

            await _hubContext.Clients.All.MonitoringStatusChanged(true);
            await _hubContext.Clients.All.MonitoringPositions(positionIds);
            return Ok();
        }

        /// <summary>
        /// stop monitor
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Roles.DeviceControl)]
        [HttpPost]
        [Route("monitor/stop")]
        public async Task<IActionResult> StopMonitor()
        {
            await _serviceBus.Send(new StopMonitorCommand());
            await _hubContext.Clients.All.MonitoringStatusChanged(false);
            return Ok();
        }

        /// <summary>
        /// monitor positions
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Roles.DeviceControl)]
        [HttpPost]
        [Route("positions/monitor")]
        public async Task<IActionResult> MonitorPositions(ICollection<int> positionIds)
        {
            await _serviceBus.Send(new StartMonitorCommand(positionIds));
            await _hubContext.Clients.All.MonitoringPositions(_monitorLoop.MonitoringPositionIds);
            return Ok();
        }

        /// <summary>
        /// cancel monitor positions
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Roles.DeviceControl)]
        [HttpPost]
        [Route("positions/cancel")]
        public async Task<IActionResult> CancelMonitorPositions(ICollection<int> positionIds)
        {
            await _serviceBus.Send(new CancelMonitorPositionsCommand(positionIds));
            await _hubContext.Clients.All.MonitorCancelledPositions(positionIds);
            return Ok();
        }

        /// <summary>
        /// 设置报警器的报警灯可用性
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Roles.DeviceControl)]
        [HttpPost]
        [Route("alerter/lighting/{enable}")]
        public async Task<IActionResult> SetAlerterLighting(bool enable)
        {
            await _serviceBus.Send(new SetAlerterLightingCommand(enable));
            await _hubContext.Clients.All.AlerterLightingChanged(enable);
            return Ok();
        }

        /// <summary>
        /// 设置报警器的报警灯可用性
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Roles.DeviceControl)]
        [HttpPost]
        [Route("alerter/buzzing/{enable}")]
        public async Task<IActionResult> SetAlerterBuzzing(bool enable)
        {
            await _serviceBus.Send(new SetAlerterBuzzingCommand(enable));
            await _hubContext.Clients.All.AlerterBuzzingChanged(enable);
            return Ok();
        }
    }
}
