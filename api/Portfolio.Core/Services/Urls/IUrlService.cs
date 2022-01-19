using System.Threading.Tasks;

namespace Portfolio.Core.Services.Urls;

public interface IUrlService
{
    Task DeleteAsync(int id);
}
