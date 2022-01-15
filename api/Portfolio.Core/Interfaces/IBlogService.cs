using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces;

public interface IBlogService
{
    Task<IEnumerable<Blog>> Get(bool includeUnPublished);

    Task Create(Blog blog);

    Task Update(Blog blog);

    Task<Blog> GetById(int id, bool includeUnPublished = false);

    Task<Blog> GetByTitle(string title, bool includeUnPublished = false);

    Task<bool> IsExistingTitle(string title, int idToIgnore = 0);

    Task Delete(Blog blog);
}

