using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models.Common;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Common;
public class SettingsService<T> : ISettingService<T> where T: BaseEntity, ISetting
{
    #region Fields

    private readonly IBaseRepository<T> _repository;
    private readonly CacheService _cacheService;
    private const string CACHE_KEY = "SETTINGS.{0}";

    #endregion

    #region Constructor

    public SettingsService(IBaseRepository<T> repository, CacheService cacheService)
    {
        _repository = repository;
        _cacheService = cacheService;
    }

    #endregion

    #region Methods

    public async Task<T> Get()
    {
        var cacheKey = GetSettingCacheKey<T>();
        var setting = _cacheService.Get<T>(cacheKey);
        if (setting != null)
            return setting;

        setting = await _repository.FirstAsync();
        if(setting != null)
            _cacheService.Set(cacheKey, setting);

        return setting;
    }

    public async Task Save(T setting)
    {
        if (setting.Id == 0)
            await _repository.InsertAsync(setting);
        else
            await _repository.UpdateAsync(setting);

        _cacheService.Set(GetSettingCacheKey<T>(), setting);
    }

    #endregion

    #region Utils

    private string GetSettingCacheKey<T>() => string.Format(CACHE_KEY, typeof(T).Name);
    

    #endregion

}
