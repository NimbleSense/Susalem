using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Core.Application.ReadModels.Auth;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Commands.Auth;

public record AuthCommand(LoginRequestDTO LoginRequest) : IRequest<Result<AuthReadModel>>;

public class AuthCommandHandler : BaseMessageHandler<AuthCommand, Result<AuthReadModel>>
{
    private readonly ILogger<AuthCommandHandler> _logger;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IApplicationConfigurationService _configurationService;

    public AuthCommandHandler(ILogger<AuthCommandHandler> logger, 
        IUserAuthenticationService userAuthenticationService,
        IApplicationConfigurationService configurationService)
    {
        _logger = logger;
        _userAuthenticationService = userAuthenticationService;
        _configurationService = configurationService;
    }

    public override async Task<Result<AuthReadModel>> HandleAsync(AuthCommand request)
    {
        var authResult = new Result<AuthReadModel>();
        var result = await _userAuthenticationService.Login(request.LoginRequest);
        if (result.Failed)
        {
            return authResult.Failed().WithMessage(result.MessageWithErrors);
        }
        var exportExcel = _configurationService.GetValueBool(Configuration.ExportExcel).Data;
        var customerName = _configurationService.GetValue(Configuration.CustomerNameKey).Data;
        return authResult.Successful().WithData(new AuthReadModel(result.Data.Token, result.Data.UserName, result.Data.Permissions, new AuthConfigReadModel(exportExcel, customerName)));
    }
}
