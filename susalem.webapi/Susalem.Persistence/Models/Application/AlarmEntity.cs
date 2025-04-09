using System;
using Susalem.Core.Application.DTOs.Record;
using System.Collections.Generic;
using Susalem.Core.Application.Enumerations;
using Susalem.Persistence.Models.Application;

namespace Susalem.Infrastructure.Models.Application
{
    public class AlarmEntity : DataEntityBase<int>
    {
        /// <summary>
        /// Alarm report time
        /// </summary>
        public DateTime ReportTime { get; set; }

        /// <summary>
        /// Alarm confirm time
        /// </summary>
        public DateTime ConfirmTime { get; set; }

        /// <summary>
        /// Warning or Alarm
        /// </summary>
        public AlarmLevelEnum Level { get; set; }

        /// <summary>
        /// Confirm content
        /// </summary>
        public string ConfirmContent { get; set; }

        /// <summary>
        /// Alarm Details
        /// </summary>
        public ICollection<AlarmDetailDTO> AlarmDetails { get; set; } = new List<AlarmDetailDTO>();

        /// <summary>
        /// Alarm is confirmed
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// The name of who confirm this alarm.
        /// </summary>
        public string ConfirmUser { get; set; }

        /// <summary>
        /// Position Name(avoid to cross db search)
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// FK for Position which this alarm belongs to
        /// </summary>
        public int PositionId { get; set; }


        /// <summary>
        /// Navigation property for this Alarm's Device.
        /// </summary>
        public virtual PositionEntity Position { get; set; }
    }
}
