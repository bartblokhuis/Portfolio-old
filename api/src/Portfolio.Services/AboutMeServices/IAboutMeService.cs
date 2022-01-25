using Portfolio.Domain.Models;
using System.Threading.Tasks;

namespace Portfolio.Services.AboutMeServices;

public interface IAboutMeService
{
    Task<AboutMe> GetAsync();

    Task SaveAsync(AboutMe model);
}

