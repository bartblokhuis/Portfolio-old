using Portfolio.Domain.Models.Settings;

namespace Portfolio.Services.Common;

public interface IEmailService
{
    Task<bool> SendEmail(string toName, string toEmailAddress, string subject, string body, EmailSettings emailSettings = null);
}

