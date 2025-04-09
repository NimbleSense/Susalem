using System;

namespace Susalem.Core.Application.ReadModels.Audit
{
    public class AuditQueryModel
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public string UserName { get; set; }

        public string EventType { get; set; }

        public string Description { get; set; }
    }
}
