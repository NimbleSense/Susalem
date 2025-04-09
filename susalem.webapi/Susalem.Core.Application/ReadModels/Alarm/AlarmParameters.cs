using System;
using Susalem.Core.Application.Enumerations;

namespace Susalem.Core.Application.ReadModels.Alarm
{
    public class AlarmParameters : QueryStringParameters, IQueryDateTimeParameters
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public AlarmLevelEnum AlarmLevel { get; set; }

        public bool ConfirmStatus { get; set; }

        //public int? PositionId { get; set; }

        //public PositionFunctionEnum? PositionFunction { get; set; }
    }
}
