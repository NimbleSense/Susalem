using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.Localize;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Commands.Monitor;

/// <summary>
/// 更新设置报警器的报警灯功能
/// </summary>
/// <param name="Enable"></param>
public record SetAlerterLightingCommand(bool Enable) : IRequest<Result>;

/// <summary>
/// 更新报警器的功能
/// </summary>
public class SetAlerterLightingCommandHandler : BaseMessageHandler<SetAlerterLightingCommand, Result>
{
    private readonly ILogger<SetAlerterLightingCommandHandler> _logger;
    private readonly IMonitorLoop _monitorLoop;
    private readonly IStringLocalizer<Resource> _localizer;

    public SetAlerterLightingCommandHandler(ILogger<SetAlerterLightingCommandHandler> logger,
        IMonitorLoop monitorLoop,
        IStringLocalizer<Resource> localizer)
    {
        _logger = logger;
        _monitorLoop = monitorLoop;
        _localizer = localizer;
    }

    public override async Task<Result> HandleAsync(SetAlerterLightingCommand request)
    {
        _logger.LogInformation($"Update alerter Lighting: {request.Enable}");
        _monitorLoop.SetAlerterLighting(request.Enable);
       
        var lighting = request.Enable ? _localizer["EnableLighting"].Value : _localizer["DisableLighting"].Value;
        return new Result().Successful();
    }
}