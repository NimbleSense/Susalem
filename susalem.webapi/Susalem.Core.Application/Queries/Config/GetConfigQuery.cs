using System.Threading.Tasks;
using AutoMapper;
using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Susalem.Core.Application.Queries.Config
{
    public record GetConfigQuery(string Id) : IRequest<Result<ApplicationConfigurationDTO>>
    {
    }

    public class GetConfigQueryHandler : BaseMessageHandler<GetConfigQuery, Result<ApplicationConfigurationDTO>>
    {
        private readonly ILogger<GetConfigQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IApplicationConfigurationService _configurationService;

        public GetConfigQueryHandler(ILogger<GetConfigQueryHandler> logger, IMapper mapper, IApplicationConfigurationService configurationService)
        {
            _logger = logger;
            _mapper = mapper;
            _configurationService = configurationService;
        }

        public override Task<Result<ApplicationConfigurationDTO>> HandleAsync(GetConfigQuery request)
        {
            return _configurationService.DetailsAsync(request.Id, true);
        }
    }
}
