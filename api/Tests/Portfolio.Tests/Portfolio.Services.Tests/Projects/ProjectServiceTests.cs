using FluentAssertions;
using NUnit.Framework;
using Portfolio.Domain.Models;
using Portfolio.Services.Projects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Tests.Portfolio.Services.Tests.Projects;

[TestFixture]
public class ProjectServiceTests : BasePortfolioTest
{
    private IProjectService _projectService;

    [OneTimeSetUp]
    public void SetUp()
    {
        _projectService = GetService<IProjectService>();

        _projectService.InsertAsync(new Project
        {
            Description = "Test project 1",
            DisplayNumber = 1,
            IsPublished = true,
            Title = "Test project 1",
        }).Wait();

        _projectService.InsertAsync(new Project
        {
            Description = "Test project 2",
            DisplayNumber = 2,
            IsPublished = false,
            Title = "Test project 2",
        }).Wait();
    }

    [Test]
    public async Task CanGetAllPublishedProjects()
    {
        var blogPosts = await _projectService.GetAllPublishedAsync();
        blogPosts.Count().Should().Be(1);
    }

    [Test]
    public async Task CanGetAllBlogPosts()
    {
        var blogPosts = await _projectService.GetAllAsync();
        blogPosts.Count().Should().Be(2);
    }

    [Test]
    public async Task ShouldHaveExistingTitle()
    {
        var isExistingTitle = await _projectService.IsExistingTitleAsync("Test project 1");
        isExistingTitle.Should().BeTrue();
    }

    [Test]
    public async Task ShouldNotHaveExistingTitleBecauseIgnoredId()
    {
        var isExistingTitle = await _projectService.IsExistingTitleAsync("Test project 1", 1);
        isExistingTitle.Should().BeFalse();
    }

    [Test]
    public async Task ShouldNotHaveExistingTitleBecauseUniqueTitle()
    {
        var isExistingTitle = await _projectService.IsExistingTitleAsync(Guid.NewGuid().ToString());
        isExistingTitle.Should().BeFalse();
    }
}
