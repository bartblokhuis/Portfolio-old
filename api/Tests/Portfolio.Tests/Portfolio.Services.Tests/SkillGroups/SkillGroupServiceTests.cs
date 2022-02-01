using FluentAssertions;
using NUnit.Framework;
using Portfolio.Domain.Models;
using Portfolio.Services.SkillGroups;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Tests.Portfolio.Services.Tests.SkillGroups;

public class SkillGroupServiceTests : BasePortfolioTest
{
    private ISkillGroupService skillGroupService;

    [OneTimeSetUp]
    public void SetUp()
    {
        skillGroupService = GetService<ISkillGroupService>();

        skillGroupService.InsertAsync(new SkillGroup
        {
            DisplayNumber = 1,
            Title = "Test skill group 1",
        }).Wait();

        skillGroupService.InsertAsync(new SkillGroup
        {
            DisplayNumber = 2,
            Title = "Test skill group 2",
        }).Wait();
    }

    [Test]
    public async Task CanGetAllSkillGroups()
    {
        var skillGroups = await skillGroupService.GetAllAsync();
        skillGroups.Count().Should().Be(2);
    }

    [Test]
    public async Task ShouldHaveExistingTitle()
    {
        var isExistingTitle = await skillGroupService.IsExistingTitleAsync("Test skill group 1");
        isExistingTitle.Should().BeTrue();
    }

    [Test]
    public async Task ShouldNotHaveExistingTitleBecauseIgnoredId()
    {
        var isExistingTitle = await skillGroupService.IsExistingTitleAsync("Test skill 1", 1);
        isExistingTitle.Should().BeFalse();
    }

    [Test]
    public async Task ShouldNotHaveExistingTitleBecauseUniqueTitle()
    {
        var isExistingTitle = await skillGroupService.IsExistingTitleAsync(Guid.NewGuid().ToString());
        isExistingTitle.Should().BeFalse();
    }
}
