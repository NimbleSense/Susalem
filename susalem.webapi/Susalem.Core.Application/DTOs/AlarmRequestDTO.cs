using Susalem.Core.Application.Enumerations;
using System;
using System.Collections.Generic;
using Susalem.Core.Application.DTOs.Record;

namespace Susalem.Core.Application.DTOs
{
    public class AlarmRequestDTO
    {
        /// <summary>
        /// Warning or Alarm
        /// </summary>
        public AlarmLevelEnum Level { get; set; }

        public IList<AlarmDetailDTO> AlarmDetails { get; set; } = new List<AlarmDetailDTO>();

        /// <summary>
        /// Alarm report time
        /// </summary>
        public DateTime ReportTime { get; set; }

        public string PositionName { get; set; }

        public int PositionId { get; set; }

        public int DeviceId { get; set; }
    }


    public class NotificationSetting
    {
        public ICollection<string> Contacts { get; set; } = new List<string>();
    }

    /// <summary>
    /// 消息类型设置
    /// </summary>
    public class RuleMessageSetting
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    /// <summary>
    /// 联系人设置
    /// </summary>
    public class ContactSetting
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
    }

    public class AlarmSetting
    {
        /// <summary>
        /// 触发间隔
        /// </summary>
        public int TriggerInterval { get; set; }

        /// <summary>
        /// 触发次数
        /// </summary>
        public int TriggerCount { get; set; }
    }
}
