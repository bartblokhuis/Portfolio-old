using Portfolio.Domain.Models;

namespace Portfolio.Services.Urls;

public interface IUrlService
{
    Task InsertAsync(Url url);

    Task UpdateAsync(Url url);

    Task DeleteAsync(int id);
}
