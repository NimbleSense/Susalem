using System;
using System.Collections.Generic;

namespace Susalem.Core.Application.ReadModels.Record
{
    public class RecordChartReadModel
    {
        /// <summary>
        /// X轴数据
        /// </summary>
        public ICollection<DateTime> XData { get; set; }

        /// <summary>
        /// Y轴左侧标题
        /// </summary>
        public string YLeftName { get; set; }

        /// <summary>
        /// Y轴右侧标题
        /// </summary>
        public string YRightName { get; set; }

        /// <summary>
        /// Y轴左侧数据
        /// </summary>
        public ICollection<YAxis> YLeft { get; set; } = new List<YAxis>();

        /// <summary>
        /// Y轴右侧数据
        /// </summary>
        public ICollection<YAxis> YRight { get; set; } = new List<YAxis>();
    }

    public class YAxis
    {
        public string Name { get; set; }

        public ICollection<double> Data { get; set; }

        public ICollection<double> MarkLineData { get; set; }

        public YAxis(string name, ICollection<double> data, ICollection<double> markLineData)
        {
            Name = name;
            Data = data;
            MarkLineData = markLineData;
        }
    }
}
