using Susalem.Common.Triggers;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;
using System.Collections.Generic;

namespace Susalem.Core.Application.ReadModels.Alarm
{
    public class AlarmRuleQueryModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 告警等级
        /// </summary>
        public AlarmLevelEnum AlarmLevel { get; set; }

        /// <summary>
        /// 通知配置
        /// </summary>
        public NotificationSetting Notification { get; set; } = new NotificationSetting();

        /// <summary>
        /// 规则设置
        /// </summary>
        public AlarmSetting Settings { get; set; }

        /// <summary>
        /// 触发条件
        /// </summary>
        public ICollection<TriggerRule> Rules { get; set; } = new List<TriggerRule>();

        /// <summary>
        /// 启用/禁用
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// 点位ID
        /// </summary>
        public int PositionId { get; set; }
    }
}
