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
/// 更新设置报警器的蜂鸣器功能
/// </summary>
/// <param name="Enable"></param>
public record SetAlerterBuzzingCommand(bool Enable) : IRequest<Result>;

/// <summary>
/// 更新报警器的功能
/// </summary>
public class SetAlerterBuzzingCommandHandler : BaseMessageHandler<SetAlerterBuzzingCommand, Result>
{
    private readonly ILogger<SetAlerterBuzzingCommandHandler> _logger;
    private readonly IMonitorLoop _monitorLoop;
    private readonly IStringLocalizer<Resource> _localizer;

    public SetAlerterBuzzingCommandHandler(ILogger<SetAlerterBuzzingCommandHandler> logger,
        IMonitorLoop monitorLoop,
        IStringLocalizer<Resource> localizer)
    {
        _logger = logger;
        _monitorLoop = monitorLoop;
        _localizer = localizer;
    }

    public override async Task<Result> HandleAsync(SetAlerterBuzzingCommand request)
    {
        _logger.LogInformation($"Update alerter Buzzing: {request.Enable}");
        _monitorLoop.SetAlerterBuzzing(request.Enable);
        

        var lighting = request.Enable ? _localizer["EnableBuzzing"].Value : _localizer["DisableBuzzing"].Value;
        return new Result().Successful();
    }
}