using System;

namespace Susalem.Core.Application.ReadModels.Message;

public class MessageRecordQueryModel
{
    public int Id { get; set; }

    public string Comment { get; set; }

    public bool Success { get; set; }

    public string AlarmRuleName { get; set; }

    public string MessageSettingName { get; set; }

    public string Target { get; set; }

    public DateTime SendTime { get; set; }

    public int AlarmId { get; set; }
}
