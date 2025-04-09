using Susalem.Core.Application.Enumerations;
using Susalem.Core.Application.ReadModels.Record;
using System;
using System.Collections.Generic;

namespace Susalem.Infrastructure.Models.Record
{
    public class PositionRecordEntity : DataEntityBase<int>
    {
        public DateTime CreateTime { get; set; }

        public ICollection<RecordContent> Contents { get; set; }

        public PositionFunctionEnum PositionFunction { get; set; }

        public string PositionName { get; set; }

        public int PositionId { get; set; }

        public string AreaName { get; set; }
    }
}
