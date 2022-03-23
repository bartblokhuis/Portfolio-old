using FluentAssertions;
using NUnit.Framework;
using Portfolio.Domain.Models.Localization;
using Portfolio.Services.Languages;
using System.Threading.Tasks;

namespace Portfolio.Tests.Portfolio.Services.Tests.Languages;

[TestFixture]
public class LanguageServiceTests : BasePortfolioTest
{
    private ILanguageService _languageService;

    [OneTimeSetUp]
    public void SetUp()
    {
        _languageService = GetService<ILanguageService>();

        _languageService.InsertAsync(new Language
        {
            Name = "Netherlands",
            DisplayNumber = 1,
            FlagImageFilePath = "",
            Id = 1,
            IsPublished = true,
            LanguageCulture = "nl-NL"
        }).Wait();
    }

    [Test]
    public async Task ShouldHaveExistingName()
    {
        var result = await _languageService.IsExistingNameOrCulture("NETHERLANDS", "");
        result.Should().BeTrue();
    }

    [Test]
    public async Task ShouldHaveExistingCulture()
    {
        var result = await _languageService.IsExistingNameOrCulture("Test", "nl-NL");
        result.Should().BeTrue();
    }

    [Test]
    public async Task ShouldHaveExistingNameAndCulture()
    {
        var result = await _languageService.IsExistingNameOrCulture("Netherlands", "nl-NL");
        result.Should().BeTrue();
    }

    [Test]
    public async Task ShouldNotHaveExistingNameOrCultureIgnoredId()
    {
        var result = await _languageService.IsExistingNameOrCulture("NETHERLANDS", "nl-NL", 1);
        result.Should().BeFalse();
    }

    [Test]
    public async Task ShouldNotHaveExistingNameOrCulture()
    {
        var result = await _languageService.IsExistingNameOrCulture("English", "en-US");
        result.Should().BeFalse();
    }

}
