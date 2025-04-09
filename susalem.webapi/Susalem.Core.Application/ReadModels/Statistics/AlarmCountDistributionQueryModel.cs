using System.Collections.Generic;

namespace Susalem.Core.Application.ReadModels.Statistics
{
    /// <summary>
    /// 告警数量时间分布
    /// </summary>
    public class PositionCountDistributionQueryModel
    {
        public List<string> PositionNames { get; set; }

        public List<int> RecordCounts { get; set; }

        public PositionCountDistributionQueryModel()
        {
            PositionNames = new List<string>();
            RecordCounts = new List<int>();
        }
    }
}
