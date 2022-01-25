using Moq;
using Portfolio.Core.Tests.Attributes;
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

        var result = await emailService.SendEmail("Bart Blokhuis", "bartblokhuis123@outlook.com", "test", "Hello World!");
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [JsonFileData("Data/email.json", "invalid", typeof(EmailSettings), typeof(bool))]
    public async Task Send_Mail_Invalid_Password(EmailSettings settings, bool expectedResult)
    {
        var settingsService = new Mock<ISettingService<EmailSettings>>();
        settingsService.Setup(service => service.Get()).Returns(() => Task.FromResult(settings));

        EmailService emailService = new EmailService(settingsService.Object);

        var result = await emailService.SendEmail("Bart Blokhuis", "bartblokhuis123@outlook.com", "test", "Hello World!");
        Assert.Equal(expectedResult, result);
    }
}

