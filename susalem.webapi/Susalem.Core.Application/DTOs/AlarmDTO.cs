using System;

namespace Susalem.Core.Application.DTOs
{
    /// <summary>
    /// Alarm transfer object
    /// </summary>
    public class AlarmDTO
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime ReportTime { get; set; }
    }

    /// <summary>
    /// Confirm alarm
    /// </summary>
    public class ConfirmAlarmDTO
    {
        public string ConfirmContent { get; set; }
    }
}
