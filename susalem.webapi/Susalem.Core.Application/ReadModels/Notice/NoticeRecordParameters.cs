using Susalem.Messages.Enumerations;
using System;
namespace Susalem.Core.Application.ReadModels.Notice;

public class NoticeRecordParameters : QueryStringParameters, IQueryDateTimeParameters
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public NotificationEnum NoticeType { get; set; }
}
