using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Services;

public class BlogPostService : IBlogPostService
{
    #region Fields

    private readonly IBaseRepository<BlogPost> _blogPostRepository;
    private readonly CacheService _cacheService;
    private const string CACHE_KEY = "BLOG.POST.";

    #endregion

    #region Constructor

    public BlogPostService(IBaseRepository<BlogPost> blogPostRepository, CacheService cacheService)
    {
        _blogPostRepository = blogPostRepository;
        _cacheService = cacheService;
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<BlogPost>> Get(bool includeUnPublished)
    {
        return includeUnPublished ?
            await _blogPostRepository.GetAsync(includeProperties: "BannerPicture,Thumbnail"):
            await _blogPostRepository.GetAsync(filter: (x) => x.IsPublished, includeProperties: "BannerPicture,Thumbnail");
    }

    public async Task Create(BlogPost model)
    {
        await _blogPostRepository.InsertAsync(model);
        _cacheService.Set(CACHE_KEY + model.Id, model);
        ClearListCache();
    }

    public async Task Update(BlogPost blogPost)
    {
        await _blogPostRepository.UpdateAsync(blogPost);
        _cacheService.Set(CACHE_KEY + blogPost.Id, blogPost);
        ClearListCache();
    }

    public async Task<BlogPost> GetById(int id, bool includeUnPublished = false)
    {
        var blogPost = _cacheService.Get<BlogPost>(CACHE_KEY + id);
        if (blogPost != null && !(!includeUnPublished && !blogPost.IsPublished))
            return blogPost;

        blogPost = includeUnPublished ?
            await (await _blogPostRepository.GetAsync(filter: (x) => x.Id == id, includeProperties: "BannerPicture,Thumbnail"))?.FirstOrDefaultAsync() :
            await (await _blogPostRepository.GetAsync(filter: (x) => x.Id == id && x.IsPublished, includeProperties: "BannerPicture,Thumbnail"))?.FirstOrDefaultAsync();

        if (blogPost != null)
            _cacheService.Set(CACHE_KEY + id, blogPost);

        return blogPost;
    }

    public async Task<BlogPost> GetByTitle(string title, bool includeUnPublished = false)
    {
        var blogPost = includeUnPublished ?
            await (await _blogPostRepository.GetAsync(filter: (x) => x.Title.ToLower() == title.ToLower(), includeProperties: "BannerPicture,Thumbnail"))?.FirstOrDefaultAsync() :
            await (await _blogPostRepository.GetAsync(filter: (x) => x.Title.ToLower() == title.ToLower() && x.IsPublished, includeProperties: "BannerPicture,Thumbnail"))?.FirstOrDefaultAsync();

        if (blogPost != null)
            _cacheService.Set(CACHE_KEY + blogPost.Id, blogPost);

        return blogPost;
    }

    public async Task Delete(BlogPost blogPost)
    {
        await _blogPostRepository.DeleteAsync(blogPost);
        ClearListCache();
        _cacheService.Set<SkillGroup>(CACHE_KEY + blogPost.Id, null);
    }

    #endregion

    #region Utils

    public Task<bool> IsExistingTitle(string title, int idToIgnore = 0)
    {
        return _blogPostRepository.Table.AnyAsync(blogPost => blogPost.Title.ToLower() == title.ToLower()
                && (idToIgnore == 0 || blogPost.Id != idToIgnore));
    }

    private void ClearListCache()
    {
        _cacheService.Set<IEnumerable<BlogPost>>(CACHE_KEY + "LIST", null);
    }

    #endregion
}

