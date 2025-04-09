using System.Collections.Generic;
using Susalem.Core.Application.ReadModels.Alarm;
using Susalem.Core.Application.ReadModels.Audit;
using Susalem.Core.Application.ReadModels.Record;

namespace Susalem.Api.Interfaces
{
    /// <summary>
    /// Generate record report
    /// </summary>
    public interface IReportService
    {
        public string GeneratePdfReport(string companyName, string reportHeader, IEnumerable<string> headers, IEnumerable<RecordReadModel> records);

        public string GenerateAuditReport(string companyName, string reportHeader, IEnumerable<string> headers, IEnumerable<AuditQueryModel> records);
        
        public string GenerateAlarmReport(string companyName, IEnumerable<AlarmDetailQueryModel> records);
    }
}
