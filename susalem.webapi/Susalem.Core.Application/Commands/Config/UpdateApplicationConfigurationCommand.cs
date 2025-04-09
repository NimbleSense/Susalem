using System.Threading.Tasks;
using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;

namespace Susalem.Core.Application.Commands.Config
{
    public record UpdateApplicationConfigurationCommand(string Id, ApplicationConfigurationDTO Dto):IRequest<Result>
    {
    }

    public class UpdateApplicationConfigurationHandler : BaseMessageHandler<UpdateApplicationConfigurationCommand, Result>
    {
        private readonly IApplicationConfigurationService _applicationConfigurationService;

        public UpdateApplicationConfigurationHandler(IApplicationConfigurationService applicationConfigurationService)
        {
            _applicationConfigurationService = applicationConfigurationService;
        }

        public override async Task<Result> HandleAsync(UpdateApplicationConfigurationCommand request)
        {
            return await _applicationConfigurationService.EditAsync(request.Dto);
        }
    }
}
