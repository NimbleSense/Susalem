using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Api.Interfaces;
using Susalem.Common.Paging;
using Susalem.Core.Application;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Alarm;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Localization;
using Susalem.Core.Application.Localize;

namespace Susalem.Api.Controllers
{
    /// <summary>
    /// Alarm management
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AlarmController : ControllerBase
    {
        private readonly IApplicationAlarmService _applicationAlarmService;
        private readonly IApplicationPositionService _applicationPositionService;
        private readonly IReportService _reportService;
        private readonly IApplicationConfigurationService _configurationService;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public AlarmController(IApplicationAlarmService applicationAlarmService,
            IApplicationPositionService applicationPositionService, 
            IReportService reportService,
            IApplicationConfigurationService configurationService,
            IStringLocalizer<Resource> stringLocalizer)
        {
            _applicationAlarmService = applicationAlarmService;
            _applicationPositionService = applicationPositionService;
            _reportService = reportService;
            _configurationService = configurationService;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        /// Get specific count unconfirmed warnings and alarms
        /// </summary>
        /// <returns></returns>
        [HttpGet("unconfirmed/{count}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IEnumerable<AlarmQueryModel>))]
        public async Task<IActionResult> GetUnConfirmedWarnings(int count)
        {
            var serviceResult = await _applicationAlarmService.GetUnConfirmedAlarmsAsync(count);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }
            return Ok(serviceResult.Data);
        }

        /// <summary>
        /// Confirm specific alarm
        /// </summary>
        /// <param name="alarmId">need to confirm Alarm ID</param>
        /// <param name="confirmAlarm">confirm content</param>
        [HttpPost("{alarmId}/confirm")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ConfirmAlarm(int alarmId, [FromBody] ConfirmAlarmDTO confirmAlarm)
        {
            var serviceResult = await _applicationAlarmService.ConfirmAlarmsAsync(new List<int>{ alarmId }, confirmAlarm.ConfirmContent);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }

            return NoContent();
        }

        /// <summary>
        /// Confirm specific alarms count
        /// </summary>
        /// <param name="count">alarm count</param>
        /// <param name="confirmAlarm">confirm content</param>
        [HttpPost("confirm/{count}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ConfirmAlarms(int count, [FromBody]ConfirmAlarmDTO confirmAlarm)
        {
            var serviceResult = await _applicationAlarmService.ConfirmAlarmsAsync(count, confirmAlarm.ConfirmContent);
            if (serviceResult.Failed)
            {
                return BadRequest(serviceResult.MessageWithErrors);
            }
            return NoContent();
        }

        /// <summary>
        /// Get all confirmed alarms by search condition
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<AlarmDetailQueryModel>))]
        public async Task<IActionResult> GetAllAlarms([FromQuery]AlarmParameters parameters)
        {
            var records = _applicationAlarmService.GetAllAlarms(parameters);
            var metadata = new
            {
                records.TotalCount,
                records.PageSize,
                records.CurrentPage,
                records.TotalPages,
                records.HasNext,
                records.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

            return Ok(records);
        }

        /// <summary>
        /// Get pdf report
        /// </summary>
        /// <param name="alarmLevel"></param>
        /// <param name="confirmStatus"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [Route("report/{alarmLevel}")]
        [HttpGet]
        public async Task<string> GetReport(AlarmLevelEnum alarmLevel,[FromQuery] bool confirmStatus, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
        {
            var result = await _applicationAlarmService.GetAllAlarms(alarmLevel, confirmStatus, startTime, endTime);
            if (result.Failed)
            {
                return string.Empty;
            }
   
            var companyName = "报告";
            var companyNameResult = _configurationService.GetValue(Configuration.CustomerNameKey);
            if (companyNameResult.Succeeded)
            {
                companyName = companyNameResult.Data;
            }
            var fileName = _reportService.GenerateAlarmReport(companyName, result.Data);

            return fileName;
        }
    }
}
