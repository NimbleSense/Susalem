using FluentEmail.Core.Models;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Susalem.Core.Application.Interfaces.Providers;
using Susalem.Core.Application.Models;
using System.Net.Mail;
using System.Net;
using Susalem.Common.Extensions;
using Susalem.Core.Application;
using Microsoft.Extensions.Logging;
using Susalem.Common.Results;

namespace Susalem.Notification.Mail.Provider;

internal class MailProvider : IMailProvider
{
    private readonly ILogger<MailProvider> _logger;

    public MailSetting Setting { get; set; }

    public MailProvider(ILogger<MailProvider> logger)
    {
        _logger = logger;
        Setting = new MailSetting();
    }

    public async Task<Result> SendAsync(string subject, string body, ICollection<string> receivers)
    {
        var result = new Result();

        using var smtpClient = new SmtpClient
        {
            Host = Setting.Server,
            Port = Setting.Port,
            UseDefaultCredentials = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = false,
            Credentials = new NetworkCredential(Setting.Sender, Setting.Code)
        };
        Email.DefaultSender = new SmtpSender(smtpClient);

        var addresses = new List<Address>();
        receivers.ForEach(t =>
        {
            if (!string.IsNullOrEmpty(t))
            {
                addresses.Add(new Address(t));
            }
        });

        var email = Email.From(Setting.Sender)
                .To(addresses)
                .Subject(subject)
                .Body(body);
        try
        {
            var response = await email.SendAsync();
            if (response is { Successful: true })
            {
                return result.Successful();
            }

            result.Failed().WithError(string.Join(",", response.ErrorMessages));
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref result, _logger, ex, "Error while trying to send mail.");
        }
        return result;
    }
}
