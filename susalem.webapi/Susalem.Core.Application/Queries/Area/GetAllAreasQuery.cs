using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces;
using Susalem.Core.Application.Interfaces.Services;

namespace Susalem.Core.Application.Queries.Area
{
    public record GetAllAreasQuery: IRequest<Result<IEnumerable<AreaDTO>>>
    {
    }

    //public class GetAllAreasQueryHandler : BaseMessageHandler<GetAllAreasQuery, Result<IEnumerable<AreaDTO>>>
    //{
    //    private readonly IApplicationDeviceService _deviceService;

    //    public GetAllAreasQueryHandler(IApplicationDeviceService deviceService)
    //    {
    //        _deviceService = deviceService;
    //    }

    //    public override Task<Result<IEnumerable<AreaDTO>>> HandleAsync(GetAllAreasQuery request)
    //    {
    //        _deviceService.GetAreasAsync();
    //    }
    //}
}
