using System;
using Susalem.Core.Application.Enumerations;

namespace Susalem.Core.Application.DTOs.Record
{
    public class AlarmDetailDTO
    {
        /// <summary>
        /// Alarm property
        /// </summary>
        public string AlarmProperty { get; set; }

        /// <summary>
        /// Alarm report value
        /// </summary>
        public double AlarmValue { get; set; }

        /// <summary>
        /// Position function
        /// </summary>
        public PositionFunctionEnum Function { get; set; }

        /// <summary>
        /// Alarm report time
        /// </summary>
        public DateTime ReportTime { get; set; }

        public AlarmDetailDTO()
        {
            ReportTime = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Invalid: {Function} {AlarmProperty} = {AlarmValue}";
        }
    }
}
