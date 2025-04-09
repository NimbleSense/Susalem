using Susalem.Core.Application;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Messages.Features.Channel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Susalem.Api.Controllers;

/// <summary>
/// 设备通道相关代码
/// </summary>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = Roles.RootManagement)]
[Route("api/[controller]")]
[ApiController]
public class ChannelController : ControllerBase
{
    private readonly IChannelService _channelService;
    private readonly IChannelFactory _channelFactory;
    private readonly IEngineFactory _engineFactory;
    private readonly ILogger<ChannelController> _logger;

    public ChannelController(IChannelService channelService,
        IChannelFactory channelFactory,
        IEngineFactory engineFactory,
        ILogger<ChannelController> logger)
    {
        _channelService = channelService;
        _channelFactory = channelFactory;
        _engineFactory = engineFactory;
        _logger = logger;
    }

    /// <summary>
    /// 获取所有通道
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var serviceResult = await _channelService.GetChannelsAsync();
        if (serviceResult.Failed)
        {
            return BadRequest(serviceResult.MessageWithErrors);
        }

        foreach (var channel in serviceResult.Data)
        {
            var commChannel = _channelFactory.GetChannel(channel.Id);
            if (commChannel != null)
            {
                channel.Status = commChannel.Status;
            }
            if(channel.ChannelType == Messages.Enumerations.ChannelEnum.ModbusTcp)
            {
                var tcpSetting = JsonConvert.DeserializeObject<TcpSetting>(channel.Content);
                channel.Title = tcpSetting?.Host;
            }
            else
            {
                var serialSetting = JsonConvert.DeserializeObject<SerialSetting>(channel.Content);
                channel.Title = serialSetting?.PortName;
            }
        }

        return Ok(serviceResult.Data);
    }

    /// <summary>
    /// Get Channel.
    /// </summary>
    /// <param name="id">channel id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetChannel(int id)
    {
        var serviceResult = await _channelService.GetChannelAsync(id);
        if (serviceResult.Failed)
        {
            return NotFound(serviceResult.MessageWithErrors);
        }

        var commChannel = _channelFactory.GetChannel(serviceResult.Data.Id);
        if (commChannel != null)
        {
            serviceResult.Data.Status = commChannel.Status;
        }

        return Ok(serviceResult.Data);
    }

    /// <summary>
    /// Get Channel.
    /// </summary>
    /// <param name="id">channel id</param>
    /// <returns></returns>
    [HttpGet("{id}/devices")]
    public async Task<IActionResult> GetDevices(int id)
    {
        var serviceResult = await _channelService.GetDevicesAsync(id);
        if (serviceResult.Failed)
        {
            return NotFound(serviceResult.MessageWithErrors);
        }

        foreach (var device in serviceResult.Data)
        {
            var engine = _engineFactory.GetEngineWithDeviceId(device.Id);
            if (engine != null)
            {
                device.Online = engine.Property.Online;
            }
        }

        return Ok(serviceResult.Data);
    }

    /// <summary>
    /// 创建一个通道
    /// </summary>
    /// <param name="channelSetting"></param>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ChannelSettingDTO channelSetting)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid payload");
        }

        var serviceResult = await _channelService.CreateChannelAsync(channelSetting);
        if (serviceResult.Failed)
        {
            return BadRequest(serviceResult.MessageWithErrors);
        }

        var createdChannelDto = serviceResult.Data;
        return CreatedAtAction(nameof(GetChannel), new { createdChannelDto.Id }, createdChannelDto);
    }

    /// <summary>
    /// 通道绑定设备
    /// </summary>
    /// <param name="channelId">通道Id</param>
    /// <returns></returns>
    [HttpPost("{channelId}/bind")]
    public async Task<IActionResult> BindDevices(int channelId)
    {
        var serviceResult = await _channelService.AddDevicesToChannelAsync(channelId);
        if (serviceResult.Failed)
        {
            return BadRequest(serviceResult.MessageWithErrors);
        }
        return Ok();
    }

    /// <summary>
    /// 通道与设备解绑
    /// </summary>
    /// <param name="channelId">通道Id</param>
    /// <returns></returns>
    [HttpPost("{channelId}/unbind")]
    public async Task<IActionResult> UnBindDevices(int channelId)
    {
        var serviceResult = await _channelService.RemoveDevicesFromChannelAsync(channelId);
        if (serviceResult.Failed)
        {
            return BadRequest(serviceResult.MessageWithErrors);
        }
        return Ok();
    }


    /// <summary>
    /// 更新一个通道
    /// </summary>
    /// <param name="id">channel id</param>
    /// <param name="channelSetting">channel配置</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ChannelSettingDTO channelSetting)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid payload");
        }

        var serviceResult = await _channelService.EditChannelAsync(id, channelSetting);
        if (serviceResult.Failed)
        {
            return BadRequest(serviceResult.MessageWithErrors);
        }

        return NoContent();
    }

    /// <summary>
    /// Delete channel
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _channelService.DeleteChannelAsync(id);
        if (serviceResult.Failed)
        {
            return NotFound(serviceResult.MessageWithErrors);
        }

        return NoContent();
    }
}
