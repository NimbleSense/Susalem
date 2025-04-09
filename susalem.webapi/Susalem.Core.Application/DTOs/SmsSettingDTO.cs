using System.ComponentModel.DataAnnotations;

namespace Susalem.Core.Application.DTOs;

public class SmsSettingDTO
{
    public string AccessKey { get; set; }
    public string AccessSecret { get; set; }

    public string TemplateId { get; set; }
}
