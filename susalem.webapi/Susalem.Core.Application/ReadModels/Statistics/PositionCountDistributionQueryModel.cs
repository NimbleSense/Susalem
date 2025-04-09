using System.Collections.Generic;

namespace Susalem.Core.Application.ReadModels.Statistics
{
    /// <summary>
    /// 告警数量时间分布
    /// </summary>
    public class AlarmCountDistributionQueryModel
    {
        public List<string> Hours { get; set; }

        public List<int> Alarms { get; set; }

        public List<int> Warnings { get; set; }

        public AlarmCountDistributionQueryModel()
        {
            Hours = new List<string>();
            Alarms = new List<int>();
            Warnings = new List<int>();
        }
    }
}
