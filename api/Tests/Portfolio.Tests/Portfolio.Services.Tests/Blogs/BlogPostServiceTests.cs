using FluentAssertions;
using NUnit.Framework;
using Portfolio.Domain.Models.Blogs;
using Portfolio.Services.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Tests.Portfolio.Services.Tests.Blogs;

[TestFixture]
public class BlogPostServiceTests : BasePortfolioTest
{
    private IBlogPostService _blogPostService;

    [OneTimeSetUp]
    public void SetUp()
    {
        _blogPostService = GetService<IBlogPostService>();

        _blogPostService.InsertAsync(new BlogPost
        {
            BannerPicture = null,
            BannerPictureId = null,
            Comments = null,
            Content = "Test blog 1",
            Description = "Test blog 1",
            CreatedAtUTC = DateTime.UtcNow,
            DisplayNumber = 1,
            IsPublished = true,
            Title = "Test blog 1",
            UpdatedAtUtc = DateTime.UtcNow,
        }).Wait();

        _blogPostService.InsertAsync(new BlogPost
        {
            BannerPicture = null,
            BannerPictureId = null,
            Comments = null,
            Content = "Test blog 2",
            Description = "Test blog 2",
            CreatedAtUTC = DateTime.UtcNow,
            DisplayNumber = 1,
            IsPublished = false,
            Title = "Test blog 2",
            UpdatedAtUtc = DateTime.UtcNow,
        }).Wait();
    }

    [Test]
    public async Task CanGetAllPublishedBlogPosts()
    {
        var blogPosts = await _blogPostService.GetPublishedBlogPostsAsync();
        blogPosts.Count().Should().Be(1);
    }
}
