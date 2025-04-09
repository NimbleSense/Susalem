using Susalem.Core.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Susalem.Common.Results;

namespace Susalem.Core.Application.Interfaces.Providers;

/// <summary>
/// 短信功能提供者
/// </summary>
public interface IMailProvider
{
    MailSetting Setting { get; set; }

    /// <summary>
    /// 邮件发送
    /// </summary>
    /// <param name="subject">标题</param>
    /// <param name="body">内容</param>
    /// <param name="receivers">收件人</param>
    /// <returns></returns>
    Task<Result> SendAsync(string subject, string body, ICollection<string> receivers);
}
