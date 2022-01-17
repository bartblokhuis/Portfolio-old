using MimeKit;
using Moq;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Core.Services.Common;
using Portfolio.Core.Tests.Attributes;
using Portfolio.Domain.Models.Settings;
using System.Threading.Tasks;
using Xunit;

namespace Portfolio.Core.Tests;

public class EmailServiceTests
{
    [Theory]
    [JsonFileData("Data/email.json", "valid", typeof(EmailSettings), typeof(bool))]
    public async Task Send_Mail_Valid(EmailSettings settings, bool expectedResult)
    {
        var settingsService = new Mock<ISettingService<EmailSettings>>();
        settingsService.Setup(service => service.Get()).Returns(() => Task.FromResult(settings));

        EmailService emailService = new EmailService(settingsService.Object);

        var result = await emailService.SendEmail(new MailboxAddress("Bart Blokhuis", "bartblokhuis123@outlook.com"), "test", new TextPart("plain") { Text = "Hello World!" });
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [JsonFileData("Data/email.json", "invalid", typeof(EmailSettings), typeof(bool))]
    public async Task Send_Mail_Invalid_Password(EmailSettings settings, bool expectedResult)
    {
        var settingsService = new Mock<ISettingService<EmailSettings>>();
        settingsService.Setup(service => service.Get()).Returns(() => Task.FromResult(settings));

        EmailService emailService = new EmailService(settingsService.Object);

        var result = await emailService.SendEmail(new MailboxAddress("Bart Blokhuis", "bartblokhuis123@outlook.com"), "test", new TextPart("plain") { Text = "Hello World!" });
        Assert.Equal(expectedResult, result);
    }
}

