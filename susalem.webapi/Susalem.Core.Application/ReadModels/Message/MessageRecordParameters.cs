using System;
namespace Susalem.Core.Application.ReadModels.Message;

public class MessageRecordParameters : QueryStringParameters, IQueryDateTimeParameters
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
