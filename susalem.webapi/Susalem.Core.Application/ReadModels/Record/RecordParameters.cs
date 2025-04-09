using System;
using Susalem.Core.Application.Enumerations;

namespace Susalem.Core.Application.ReadModels.Record
{
    public class RecordParameters : QueryStringParameters,IQueryDateTimeParameters
    {
        public int PositionId { get; set; }

        public PositionFunctionEnum PositionFunction { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class RecordChartRequestDTO
    {
        public int PositionId { get; set; }

        public PositionFunctionEnum PositionFunction { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
