using System;
using System.Collections.Generic;
using Susalem.Common.Triggers;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Enumerations;

namespace Susalem.Infrastructure.Models.Application
{
    /// <summary>
    /// 告警规则配置
    /// </summary>
    public class AlarmRuleEntity:DataEntityBase<int>
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 通知配置
        /// </summary>
        public NotificationSetting Notification{ get; set; }

        /// <summary>
        /// 告警等级
        /// </summary>
        public AlarmLevelEnum AlarmLevel { get; set; }

        /// <summary>
        /// 触发条件
        /// </summary>
        public ICollection<TriggerRule> Rules { get; set; } = new List<TriggerRule>();

        /// <summary>
        /// 规则设置
        /// </summary>
        public AlarmSetting Settings { get; set; }

        /// <summary>
        /// 启用/禁用
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// FK for Position which this alarm belongs to
        /// </summary>
        public int PositionId { get; set; }

        public virtual PositionEntity Position { get; set; }
    }
}
