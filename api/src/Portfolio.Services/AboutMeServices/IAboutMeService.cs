using Portfolio.Domain.Models;

namespace Portfolio.Services.AboutMeServices;

public interface IAboutMeService
{
    Task<AboutMe> GetAsync();

    Task SaveAsync(AboutMe model);
}

