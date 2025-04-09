using System;

namespace Susalem.Core.Application.ReadModels.Audit
{
    public class AuditParameters: QueryStringParameters, IQueryDateTimeParameters
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
