using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Commands.Auth;

public record UnAuthCommand() : IRequest<Result>;

public class UnAuthCommandHandler : BaseMessageHandler<UnAuthCommand, Result>
{
    private readonly IUserAuthenticationService _userAuthenticationService;

    public UnAuthCommandHandler(IUserAuthenticationService userAuthenticationService) 
    {
        _userAuthenticationService = userAuthenticationService;
    }

    public override async Task<Result> HandleAsync(UnAuthCommand request)
    {
        return await _userAuthenticationService.Logout();
    }
}
