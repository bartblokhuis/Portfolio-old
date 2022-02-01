using FluentAssertions;
using NUnit.Framework;
using Portfolio.Domain.Models;
using Portfolio.Services.SkillGroups;
using Portfolio.Services.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Tests.Portfolio.Services.Tests.Skills;

[TestFixture]
public class SkillServiceTests : BasePortfolioTest
{
    private ISkillService _skillService;
    private SkillGroup _testSkillGroup1;
    private SkillGroup _testSkillGroup2;

    private Skill _testSkill1;
    private Skill _testSkill2;
    private Skill _testSkill3;

    [OneTimeSetUp]
    public void SetUp()
    {
        var skillGroupService = GetService<ISkillGroupService>();
        _testSkillGroup1 = new SkillGroup
        {
            Title = "Test skill group 1"
        };

        _testSkillGroup2 = new SkillGroup
        {
            Title = "Test skill group 1"
        };

        skillGroupService.InsertAsync(_testSkillGroup1).Wait();
        skillGroupService.InsertAsync(_testSkillGroup2).Wait();

        _skillService = GetService<ISkillService>();

        _skillService.InsertAsync(_testSkill1 = new Skill
        {
            DisplayNumber = 1,
            Name = "Test skill 1",
            SkillGroupId = _testSkillGroup1.Id
        }).Wait();

        _skillService.InsertAsync(_testSkill2 = new Skill
        {
            DisplayNumber = 2,
            Name = "Test skill 2",
            SkillGroupId = _testSkillGroup1.Id
        }).Wait();

        _skillService.InsertAsync(_testSkill3 = new Skill
        {
            DisplayNumber = 2,
            Name = "Test skill 2",
            SkillGroupId = _testSkillGroup2.Id
        }).Wait();
    }

    [Test]
    public async Task CanGetAllSkills()
    {
        var skills = await _skillService.GetAllAsync();
        skills.Count().Should().Be(3);
    }

    [Test]
    public async Task CanGetAllSkillsBySkillGroup()
    {
        var skills1 = await _skillService.GetBySkillGroupIdAsync(_testSkillGroup1.Id);
        skills1.Count().Should().Be(2);

        var skills2 = await _skillService.GetBySkillGroupIdAsync(_testSkillGroup2.Id);
        skills2.Count().Should().Be(1);
    }

    [Test]
    public async Task CanGetSkillsById()
    {
        var skills = await _skillService.GetSkillsByIdsAsync(new List<int> { _testSkill1.Id, _testSkill2.Id, _testSkill3.Id });
        skills.Count().Should().Be(3);
    }

    [Test]
    public async Task CanGetSkillById()
    {
        var skill = await _skillService.GetByIdAsync(_testSkill1.Id);
        skill.Should().NotBeNull();
    }

    [Test]
    public async Task ShouldExist()
    {
        var exist = await _skillService.ExistsAsync(_testSkill1.Id);
        exist.Should().BeTrue();
    }

    [Test]
    public async Task ShouldNotExist()
    {
        var exist = await _skillService.ExistsAsync(int.MaxValue);
        exist.Should().BeFalse();
    }

    [Test]
    public async Task ShouldHaveExistingTitle()
    {
        var isExistingTitle = await _skillService.IsExistingSkillAsync("Test skill 1", _testSkillGroup1.Id);
        isExistingTitle.Should().BeTrue();
    }

    [Test]
    public async Task ShouldNotHaveExistingTitleBecauseIgnoredId()
    {
        var isExistingTitle = await _skillService.IsExistingSkillAsync("Test skill 1", _testSkillGroup1.Id, _testSkill1);
        isExistingTitle.Should().BeFalse();
    }

    [Test]
    public async Task ShouldNotHaveExistingTitleBecauseUniqueTitle()
    {
        var isExistingTitle = await _skillService.IsExistingSkillAsync(Guid.NewGuid().ToString(), _testSkillGroup1.Id);
        isExistingTitle.Should().BeFalse();
    }
}
