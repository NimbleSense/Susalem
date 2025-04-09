using System.Collections.Generic;

namespace Susalem.Core.Application.DTOs
{
    public class AuditRequestDTO
    {
        public string Operator { get; set; }

        public string Description { get; set; }

        public AuditHistoryDetails HistoryDetails { get; set; } = new AuditHistoryDetails();
    }


    /// <summary>
    /// Changed values details
    /// </summary>
    public class AuditHistoryDetails
    {
        /// <summary>
        /// The values after action.
        /// Key contains column name and Value the value of the column.
        /// </summary>
        public Dictionary<string, object> NewValues { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// The values before the action.
        /// Key contains column name and Value the value of the column.
        /// </summary>
        public Dictionary<string, object> OldValues { get; set; } = new Dictionary<string, object>();
    }
}
