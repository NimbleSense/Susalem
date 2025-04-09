using System.Collections.Generic;
using Susalem.Core.Application.DTOs;

namespace Susalem.Infrastructure.Models.Application
{
    /// <summary>
    /// Point position entity
    /// </summary>
    public class PositionEntity : DataEntityBase<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IList<PositionFunctionProperty> Functions { get; set; } = new List<PositionFunctionProperty>();

        /// <summary>
        /// 绑定的报警器
        /// </summary>
        public ICollection<int> BoundedAlerter { get; set; } = new List<int>();

        /// <summary>
        /// FK for Area which this device belongs to.
        /// </summary>
        public int AreaId { get; set; }

        public bool ShowDoor { get; set; } 

        /// <summary>
        /// Navigation property for this device's Area.
        /// </summary>
        public virtual AreaEntity Area { get; set; }

        public virtual ICollection<AlarmEntity> Alarms { get; set; } = new List<AlarmEntity>();

        public virtual ICollection<AlarmRuleEntity> Rules { get; set; } = new List<AlarmRuleEntity>();

    }
}
