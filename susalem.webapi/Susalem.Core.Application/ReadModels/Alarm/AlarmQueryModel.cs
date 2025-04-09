using System;
using Susalem.Core.Application.DTOs.Record;
using System.Collections.Generic;
using Susalem.Core.Application.Enumerations;

namespace Susalem.Core.Application.ReadModels.Alarm
{
    public class AlarmQueryModel
    {
        public int Id { get; set; }

        public DateTime ReportTime { get; set; }

        public string PositionName { get; set; }

        public ICollection<AlarmDetailDTO> AlarmDetails { get; set; } = new List<AlarmDetailDTO>();

        public AlarmLevelEnum Level { get; set; }
    }
}
