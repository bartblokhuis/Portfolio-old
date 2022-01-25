using Portfolio.Domain.Models.Blogs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Blogs;

public interface IBlogPostService
{
    Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
    Task<IEnumerable<BlogPost>> GetPublishedBlogPostsAsync();

    Task InsertAsync(BlogPost blogPost);

    Task UpdateAsync(BlogPost blogPost);

    Task<BlogPost> GetByIdAsync(int id, bool includeUnPublished = false);

    Task<BlogPost> GetByTitleAsync(string title, bool includeUnPublished = false);

    Task<bool> IsExistingTitleAsync(string title, int idToIgnore = 0);

    Task DeleteAsync(BlogPost blogPost);
}

