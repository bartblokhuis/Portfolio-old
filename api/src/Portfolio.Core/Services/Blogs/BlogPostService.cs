using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models.Blogs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Blogs;

public class BlogPostService : IBlogPostService
{
    #region Fields

    private readonly IBaseRepository<BlogPost> _blogPostRepository;

    #endregion

    #region Constructor

    public BlogPostService(IBaseRepository<BlogPost> blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
    {
        return await _blogPostRepository.GetAllAsync(query => query.Include(x => x.Thumbnail).Include(x => x.BannerPicture).Include(x => x.Comments)
        .OrderByDescending(x => x.DisplayNumber).ThenBy(x => x.CreatedAtUTC),
            cache => cache.PrepareKeyForDefaultCache(BlogPostDefaults.AllBlogPostsCacheKey));
    }

    public async Task<IEnumerable<BlogPost>> GetPublishedBlogPostsAsync()
    {
        return await _blogPostRepository.GetAllAsync(query => query.Where(x => x.IsPublished).Include(x => x.Thumbnail).Include(x => x.BannerPicture).OrderByDescending(x => x.DisplayNumber).ThenBy(x => x.CreatedAtUTC),
            cache => cache.PrepareKeyForDefaultCache(BlogPostDefaults.PublishedBlogPostsCacheKey));
    }

    public async Task<BlogPost> GetByIdAsync(int id, bool includeUnPublished = false)
    {
        var blogPosts = await _blogPostRepository.GetAllAsync(query => query
            .Include(x => x.Thumbnail)
            .Include(x => x.BannerPicture)
            .Include(x => x.Comments.OrderByDescending(x => x.CreatedAtUTC))
            .ThenInclude(x => x.Comments.OrderByDescending(x => x.CreatedAtUTC))
            .ThenInclude(x => x.Comments.OrderByDescending(x => x.CreatedAtUTC))
            .ThenInclude(x => x.Comments.OrderByDescending(x => x.CreatedAtUTC))
            .ThenInclude(x => x.Comments.OrderByDescending(x => x.CreatedAtUTC))
            .OrderByDescending(x => x.DisplayNumber).ThenBy(x => x.CreatedAtUTC)
            .Where(x => x.Id == id));

        if (blogPosts == null)
            return null;

        var blogPost = blogPosts.FirstOrDefault();
        if (blogPost == null || (!blogPost.IsPublished && !includeUnPublished))
            return null;

        return blogPost;
    }

    public async Task<BlogPost> GetByTitleAsync(string title, bool includeUnPublished = false)
    {
        //TODO: Add support for more child comments.
        var blogPosts = await _blogPostRepository.GetAllAsync(query => query
        .Include(x => x.Thumbnail)
        .Include(x => x.BannerPicture)
        .Include(x => x.Comments.OrderByDescending(x => x.CreatedAtUTC))
        .ThenInclude(x => x.Comments.OrderByDescending(x => x.CreatedAtUTC))
        .ThenInclude(x => x.Comments.OrderByDescending(x => x.CreatedAtUTC))
        .ThenInclude(x => x.Comments.OrderByDescending(x => x.CreatedAtUTC))
        .ThenInclude(x => x.Comments.OrderByDescending(x => x.CreatedAtUTC))
        .OrderByDescending(x => x.DisplayNumber).ThenBy(x => x.CreatedAtUTC)
        .Where(x => x.Title.ToLower() == title.ToLower()));
        if(blogPosts == null) 
            return null;

        var blogPost = blogPosts.First();

        if (blogPost == null || (!blogPost.IsPublished && !includeUnPublished))
            return null;

        return blogPost;
    }

    public async Task InsertAsync(BlogPost model)
    {
        await _blogPostRepository.InsertAsync(model);
    }

    public async Task UpdateAsync(BlogPost blogPost)
    {
        await _blogPostRepository.UpdateAsync(blogPost);
    }

    public async Task DeleteAsync(BlogPost blogPost)
    {
        await _blogPostRepository.DeleteAsync(blogPost);
    }

    public Task<bool> IsExistingTitleAsync(string title, int idToIgnore = 0)
    {
        return _blogPostRepository.Table.AnyAsync(blogPost => blogPost.Title.ToLower() == title.ToLower()
                && (idToIgnore == 0 || blogPost.Id != idToIgnore));
    }

    #endregion
}

