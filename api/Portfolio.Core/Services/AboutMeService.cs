using Portfolio.Core.Interfaces;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models;
using System.Threading.Tasks;

namespace Portfolio.Core.Services;

public class AboutMeService : IAboutMeService
{
    #region Fields

    private readonly IBaseRepository<AboutMe> _aboutMeRepository;
    private readonly CacheService _cacheService;
    private const string CACHE_KEY = "ABOUT.ME";

    #endregion

    #region Constructor

    public AboutMeService(IBaseRepository<AboutMe> aboutMeRepository, CacheService cacheService)
    {
        _aboutMeRepository = aboutMeRepository;
        _cacheService = cacheService;
    }

    #endregion

    #region Methods

    public async Task<AboutMe> GetAboutMe()
    {
        var aboutMe = _cacheService.Get<AboutMe>(CACHE_KEY);
        if (aboutMe != null)
            return aboutMe;

        aboutMe = await _aboutMeRepository.FirstAsync();
        if(aboutMe != null)
            _cacheService.Set<AboutMe>(CACHE_KEY, aboutMe);

        return aboutMe;
    }

    public async Task SaveAboutMe(AboutMe model)
    {
        var aboutMe = await GetAboutMe();
        if(aboutMe == null)
            await _aboutMeRepository.InsertAsync(model);
        else
            await Update(model, aboutMe);

        _cacheService.Set<AboutMe>(CACHE_KEY, aboutMe);
    }

    #region Utils

    private Task Update(AboutMe model, AboutMe original)
    {
        original.Content = model.Content;
        original.Title = model.Title;

        return _aboutMeRepository.UpdateAsync(original);
    }

    #endregion

    #endregion

}
