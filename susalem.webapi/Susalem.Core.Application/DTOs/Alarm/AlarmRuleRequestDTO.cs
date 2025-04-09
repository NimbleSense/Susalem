using Susalem.Common.Triggers;
using Susalem.Core.Application.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Core.Application.DTOs.Alarm
{
    public class AlarmRuleRequestDTO
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 描述备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 告警等级
        /// </summary>
        [Required]
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
        /// 点位Id
        /// </summary>
        public int PositionId { get; set; }
    }
}
