using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;
using System.Threading.Tasks;
using Susalem.Core.Application.Interfaces.Services;

namespace Susalem.Core.Application.Commands.Users
{
    public record CreateUserCommand():IRequest<Result>
    {
    }

    public class CreateUserCommandHandler : BaseMessageHandler<CreateUserCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public CreateUserCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public override Task<Result> HandleAsync(CreateUserCommand request)
        {
            throw new System.NotImplementedException();
        }
    }
}
