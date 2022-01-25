using Portfolio.Domain.Models;
using System.Threading.Tasks;

namespace Portfolio.Services.Urls;

public interface IUrlService
{

    Task UpdateAsync(Url url);

    Task DeleteAsync(int id);
}
