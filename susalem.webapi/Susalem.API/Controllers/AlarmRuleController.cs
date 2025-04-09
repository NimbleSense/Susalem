using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.DTOs.Alarm;
using Susalem.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Susalem.Api.Controllers;

/// <summary>
/// Related with alarm rulle
/// </summary>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
public class AlarmRuleController : ControllerBase
{
    private readonly ILogger<AlarmRuleController> _logger;
    private readonly IAlarmRuleService _alarmRuleService;

    /// <summary>
    /// Constructor
    /// </summary>
    public AlarmRuleController(ILogger<AlarmRuleController> logger,
        IAlarmRuleService alarmRuleService)
    {
        _logger = logger;
        _alarmRuleService = alarmRuleService;
    }

    /// <summary>
    /// Get All alarm rules
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAlarmRules()
    {
        var serviceResult = await _alarmRuleService.GetAlarmRulesAsync();
        if (serviceResult.Failed)
        {
            return BadRequest(serviceResult.MessageWithErrors);
        }
        return Ok(serviceResult.Data);
    }

    /// <summary>
    /// Get alarm rule by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAlarmRule(int id)
    {
        var serviceResult = await _alarmRuleService.GetAlarmRuleAsync(id);
        if (serviceResult.Failed)
        {
            return NotFound(serviceResult.MessageWithErrors);
        }

        return Ok(serviceResult.Data);
    }

    /// <summary>
    /// Create a new alarm rule
    /// </summary>
    /// <param name="alarmRuleDto"></param>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AlarmRuleRequestDTO alarmRuleDto)
    {
        var serviceResult = await _alarmRuleService.CreateAlarmRuleAsync(alarmRuleDto);
        if (serviceResult.Failed)
        {
            return BadRequest(serviceResult.MessageWithErrors);
        }

        var newDto = serviceResult.Data;
        return CreatedAtAction(nameof(GetAlarmRule), new { Id = newDto.Id }, newDto);
    }

    /// <summary>
    /// Update a alarm rule
    /// </summary>
    /// <param name="id"></param>
    /// <param name="alarmRuleDto"></param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] AlarmRuleRequestDTO alarmRuleDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid payload");
        }

        var serviceResult = await _alarmRuleService.EditAlarmRuleAsync(id, alarmRuleDto);
        if (serviceResult.Failed)
        {
            return BadRequest(serviceResult.MessageWithErrors);
        }

        return NoContent();
    }

    /// <summary>
    /// Delete alarm rule
    /// </summary>
    /// <param name="id">alarm rule id</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _alarmRuleService.DeleteAlarmRuleAsync(id);
        if (serviceResult.Failed)
        {
            return NotFound(serviceResult.MessageWithErrors);
        }

        return NoContent();
    }

    /// <summary>
    /// Update alarm rule notification users
    /// </summary>
    /// <param name="id"></param>
    /// <param name="settings"></param>
    [HttpPut("{id}/notification")]
    public async Task<IActionResult> SetNotification(int id, [FromBody] NotificationSetting settings)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid payload");
        }

        var serviceResult = await _alarmRuleService.SetAlarmRuleNotificationAsync(id, settings);
        if (serviceResult.Failed)
        {
            return BadRequest(serviceResult.MessageWithErrors);
        }

        return NoContent();
    }

    ///// <summary>
    ///// Get all position by area id
    ///// </summary>
    ///// <param name="areaId">area id</param>
    ///// <returns></returns>
    //[HttpGet("{areaId}/positions")]
    //public async Task<IActionResult> GetPositions(int areaId)
    //{
    //    var serviceResult = await _applicationPositionService.GetPositionsByAreaIdAsync(areaId);
    //    if (serviceResult.Failed)
    //    {
    //        return NotFound(serviceResult.MessageWithErrors);
    //    }

    //    return Ok(serviceResult.Data);
    //}

    ///// <summary>
    ///// Get all monitor areas include position and functions
    ///// </summary>
    ///// <returns></returns>
    //[HttpGet("monitor")]
    //public async Task<IActionResult> GetAllMonitorAreas()
    //{
    //    var serviceResult = await _applicationPositionService.GetMonitorAreasAsync();
    //    if (serviceResult.Failed)
    //    {
    //        return NotFound(serviceResult.MessageWithErrors);
    //    }

    //    return Ok(serviceResult.Data);
    //}
}