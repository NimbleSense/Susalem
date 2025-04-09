using Susalem.Common.Paging;
using Susalem.Common.Results;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.ReadModels.Record;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Susalem.Core.Application.Interfaces.Services;

public interface IRecordService
{
    Task<Result> CreateRecordsAsync(IEnumerable<RecordRequestDTO> recordRequests);

    PagedList<RecordReadModel> GetRecords(RecordParameters parameters);

    Task<Result<IEnumerable<RecordReadModel>>> GetRecordsAsync(int positionId, PositionFunctionEnum positionFunction, DateTime startTime, DateTime endTime);
    
    Task<Result<RecordChartReadModel>> GetChartRecordsAsync(RecordChartRequestDTO request);

    int GetRecordCountWithinTime(int positionId, DateTime startTime, DateTime endTime);
}
