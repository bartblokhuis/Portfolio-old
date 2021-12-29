using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Services;

public class BlogService : IBlogService
{
    #region Fields

    private readonly IBaseRepository<Blog> _blogRepository;
    private readonly CacheService _cacheService;
    private const string CACHE_KEY = "BLOG.";

    #endregion

    #region Constructor

    public BlogService(IBaseRepository<Blog> blogRepository, CacheService cacheService)
    {
        _blogRepository = blogRepository;
        _cacheService = cacheService;
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<Blog>> Get(bool includeUnPublished)
    {
        return includeUnPublished ?
            await _blogRepository.GetAsync():
            await _blogRepository.GetAsync(filter: (x) => x.IsPublished);
    }

    public async Task Create(Blog model)
    {
        await _blogRepository.InsertAsync(model);
        _cacheService.Set(CACHE_KEY + model.Id, model);
        ClearListCache();
    }

    public async Task Update(Blog blog)
    {
        await _blogRepository.UpdateAsync(blog);
        _cacheService.Set(CACHE_KEY + blog.Id, blog);
        ClearListCache();
    }

    public async Task<Blog> GetById(int id, bool includeUnPublished = false)
    {
        var blog = _cacheService.Get<Blog>(CACHE_KEY + id);
        if (blog != null && !(!includeUnPublished && !blog.IsPublished))
            return blog;

        blog = includeUnPublished ?
            await (await _blogRepository.GetAsync(filter: (x) => x.Id == id))?.FirstOrDefaultAsync() :
            await (await _blogRepository.GetAsync(filter: (x) => x.Id == id && x.IsPublished))?.FirstOrDefaultAsync();

        if (blog != null)
            _cacheService.Set(CACHE_KEY + id, blog);

        return blog;
    }

    public async Task Delete(Blog blog)
    {
        await _blogRepository.DeleteAsync(blog);
        ClearListCache();
        _cacheService.Set<SkillGroup>(CACHE_KEY + blog.Id, null);
    }

    #endregion

    #region Utils

    public Task<bool> IsExistingTitle(string title, int idToIgnore = 0)
    {
        return _blogRepository.Table.AnyAsync(blogPost => blogPost.Title.ToLower() == title.ToLower()
                && (idToIgnore == 0 || blogPost.Id != idToIgnore));
    }

    private void ClearListCache()
    {
        _cacheService.Set<IEnumerable<Blog>>(CACHE_KEY + "LIST", null);
    }

    #endregion
}

