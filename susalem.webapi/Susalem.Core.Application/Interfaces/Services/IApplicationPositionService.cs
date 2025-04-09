using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.ReadModels.Position;

namespace Susalem.Core.Application.Interfaces.Services
{
    /// <summary>
    /// Area and position management.
    /// </summary>
    public interface IApplicationPositionService
    {

        #region Area

        Task<Result<AreaDTO>> CreateAreaAsync(AreaDTO areaDto);

        Task<Result> EditAreaAsync(int areaId, AreaDTO areaDto);

        Task<Result> DeleteAreaAsync(int areaId);

        Task<Result<IEnumerable<AreaDTO>>> GetAreasAsync();

        Task<Result<AreaDTO>> GetAreaAsync(int areaId);

        Task<Result<IEnumerable<MonitorAreaQueryModel>>> GetMonitorAreasAsync();

        #endregion

        #region Position

        Task<Result<IEnumerable<PositionQueryModel>>> GetPositionsAsync();

        Task<Result<IEnumerable<PositionQueryModel>>> GetPositionsAsync(ICollection<int> positionIds);

        Task<Result<PositionQueryModel>> GetPositionAsync(int positionId);

        Task<Result<IEnumerable<PositionQueryModel>>> GetPositionsByAreaIdAsync(int areaId);

        Task<Result<PositionQueryModel>> CreatePositionAsync(PositionRequestDTO positionDto);

        Task<Result> EditPositionAsync(int positionId, PositionRequestDTO positionDto);

        Task<Result> DeletePositionAsync(int positionId);

        Task<Result> EditPositionFunctionAsync(int positionId,List<PositionFunctionProperty> positionFunctions);

        #endregion

        #region Device

        Task<Result> UpdateFunctionAsync(int positionId, IList<PositionFunctionProperty> positionFunctions);

        Task<Result> DeleteFunctionAsync(int positionId, PositionFunctionEnum positionFunction);

        #endregion

    }
}
