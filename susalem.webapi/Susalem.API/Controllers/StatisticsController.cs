using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Susalem.Api.Controllers;

/// <summary>
/// 统计信息类
/// </summary>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    /// <summary>
    /// 基础统计信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("basic")]
    public IActionResult GetBasicInfo()
    {
        var result = _statisticsService.GetBasicInfo();
        if (result.Failed)
        {
            return BadRequest(result.MessageWithErrors);
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// 点位功能类型分布
    /// </summary>
    /// <returns></returns>
    [HttpGet("position/distribution")]
    public async Task<IActionResult> GetDistribution()
    {
        var result = await _statisticsService.GetPositionDistributionAsync();
        if (result.Failed)
        {
            return BadRequest(result.MessageWithErrors);
        }
        return Ok(result.Data);
    }

    /// <summary>
    /// 点位功能类型分布
    /// </summary>
    /// <returns></returns>
    [HttpGet("alarm/day")]
    public IActionResult GetAlarmCountDistribution()
    {
        var result = _statisticsService.GetAlarmCountDistributionByDay();
        if (result.Failed)
        {
            return BadRequest(result.MessageWithErrors);
        }
        return Ok(result.Data);
    }

    /// <summary>
    /// 点位数据量分布
    /// </summary>
    /// <returns></returns>
    [HttpGet("position/day")]
    public IActionResult GetPositionCountDistribution()
    {
        var result = _statisticsService.GetPositionCountDistributionByDay();
        if (result.Failed)
        {
            return BadRequest(result.MessageWithErrors);
        }
        return Ok(result.Data);
    }
}