using Susalem.Common.Results;
using Susalem.Core.Application.Interfaces;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Commands.Users
{
    public record ChangeUserPasswordCommand() :IRequest<Result>
    {
    }

    public class ChangeUserPasswordCommandHandler : BaseMessageHandler<ChangeUserPasswordCommand, Result>
    {
        public override Task<Result> HandleAsync(ChangeUserPasswordCommand request)
        {
            throw new System.NotImplementedException();
        }
    }
}
