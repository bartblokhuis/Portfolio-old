using MailKit.Net.Smtp;
using MimeKit;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Common;

public class EmailService : IEmailService
{
    #region Properties

    private readonly ISettingService<EmailSettings> _emailSettingsService;

    #endregion

    #region Constructor

    public EmailService(ISettingService<EmailSettings> emailSettingsService)
    {
        _emailSettingsService = emailSettingsService;
    }

    #endregion

    #region Methods

    public async Task<bool> SendEmail(MailboxAddress toAddress, MimeEntity body)
    {
        var settings = await _emailSettingsService.Get();
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(settings.DisplayName, settings.Email));
        message.To.Add(toAddress);
        message.Subject = "Test";

        message.Body = body;

        try
        {
            var client = new SmtpClient();
            await client.ConnectAsync(settings.Host, settings.Port, settings.EnableSsl);

            if (!string.IsNullOrEmpty(settings.Username) && !string.IsNullOrEmpty(settings.Password))
            {
                await client.AuthenticateAsync(settings.Username, settings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
        
    }

    #endregion
}
