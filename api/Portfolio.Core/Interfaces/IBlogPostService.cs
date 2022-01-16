using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces;

public interface IBlogPostService
{
    Task<IEnumerable<BlogPost>> Get(bool includeUnPublished);

    Task Create(BlogPost blogPost);

    Task Update(BlogPost blogPost);

    Task<BlogPost> GetById(int id, bool includeUnPublished = false);

    Task<BlogPost> GetByTitle(string title, bool includeUnPublished = false);

    Task<bool> IsExistingTitle(string title, int idToIgnore = 0);

    Task Delete(BlogPost blogPost);
}

