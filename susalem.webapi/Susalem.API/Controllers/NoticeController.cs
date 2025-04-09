using Susalem.Core.Application;
using Susalem.Core.Application.Commands.Config;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Providers;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Localize;
using Susalem.Core.Application.Models;
using Susalem.Core.Application.Notification;
using Susalem.Core.Application.ReadModels.Notice;
using Susalem.Messages.Enumerations;
using Susalem.Shared.Messages.Features.Notice;
using Susalem.Shared.Messages.Features.Notification;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Susalem.Api.Controllers;

/// <summary>
/// 消息通知功能
/// </summary>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = Roles.Notification)]
[Route("api/[controller]")]
[ApiController]
public class NoticeController : ControllerBase
{
    private readonly IMailProvider _mailProvider;
    private readonly WebhookSetting _webhookSetting;
    private readonly IApplicationConfigurationService _configurationService;
    private readonly IApplicationPositionService _positionService;
    private readonly IServiceBus _serviceBus;
    private readonly IStringLocalizer<Resource> _stringLocalizer;

    public NoticeController(IApplicationPositionService positionService, 
        IMailProvider mailProvider,
        WebhookSetting webhookSetting,
        IApplicationConfigurationService configurationService,  
        IServiceBus serviceBus,
        IStringLocalizer<Resource> stringLocalizer)
    {
        _positionService = positionService;
        _mailProvider = mailProvider;
        _webhookSetting = webhookSetting;
        _configurationService = configurationService;
        _serviceBus = serviceBus;
        _stringLocalizer = stringLocalizer;
    }

    #region 邮件功能

    /// <summary>
    /// 获取邮件配置
    /// </summary>
    /// <returns></returns>
    [HttpGet("mail")]
    public async Task<IActionResult> GetMail()
    {
        var mailSetting = _configurationService.GetValue<MailSetting>(Configuration.MailSettingKey);
        return Ok(mailSetting);
    }

    /// <summary>
    /// 邮件配置
    /// </summary>
    /// <param name="setting"></param>
    /// <returns></returns>
    [HttpPost("mail/setting")]
    public async Task<IActionResult> EditMail([FromBody] MailSetting setting)
    {
        var configDto = new ApplicationConfigurationDTO
        {
            Id = Configuration.MailSettingKey,
            Value = JsonConvert.SerializeObject(setting)
        };

        var result = await _serviceBus.Send(new UpdateApplicationConfigurationCommand(configDto.Id, configDto));
        if (result.Failed)
        {
            return BadRequest();
        }
        _mailProvider.Setting = setting;
        return NoContent();
    }

    /// <summary>
    /// 发送邮件测试
    /// </summary>
    /// <returns></returns>
    [HttpPost("mail/test")]
    public async Task<IActionResult> TestMail(SendMailRequest request)
    {
        var result = await _mailProvider.SendAsync(request.Subject, request.Body, new Collection<string> { request.Receiver });
        if (result.Failed) return BadRequest(); 

        return Ok();
    }


    #endregion

    #region 短信功能

    #endregion
}
