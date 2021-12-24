using MimeKit;
using Portfolio.Domain.Models;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Common;

public interface IEmailService
{
    Task<bool> SendEmail(MailboxAddress toAddress, MimeEntity body, EmailSettings emailSettings = null);
}

