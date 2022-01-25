using Portfolio.Domain.Models;

namespace Portfolio.Services.Urls;

public interface IUrlService
{

    Task UpdateAsync(Url url);

    Task DeleteAsync(int id);
}
