using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Models.Common;

namespace Portfolio.Services.Blogs;

public interface IBlogPostService
{
    Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
    Task<IEnumerable<BlogPost>> GetPublishedBlogPostsAsync();

    Task<IPagedList<BlogPost>> GetAllBlogPostsAsync(int pageIndex = 0, int pageSize = int.MaxValue);

    Task InsertAsync(BlogPost blogPost);

    Task UpdateAsync(BlogPost blogPost);

    Task<BlogPost> GetByIdAsync(int id, bool includeUnPublished = false);

    Task<BlogPost> GetByTitleAsync(string title, bool includeUnPublished = false);

    Task<bool> IsExistingTitleAsync(string title, int idToIgnore = 0);

    Task DeleteAsync(BlogPost blogPost);
}

