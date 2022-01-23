using Portfolio.Domain.Models.Settings;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Common;

public interface IEmailService
{
    Task<bool> SendEmail(string toName, string toEmailAddress, string subject, string body, EmailSettings emailSettings = null);
}

