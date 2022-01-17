using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models.Common;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Settings;
public class SettingsService<T> : ISettingService<T> where T: BaseEntity, ISetting
{
    #region Fields

    private readonly IBaseRepository<T> _repository;

    #endregion

    #region Constructor

    public SettingsService(IBaseRepository<T> repository)
    {
        _repository = repository;
    }

    #endregion

    #region Methods

    public async Task<T> Get()
    {
        var setting = await _repository.FirstAsync(cache => cache.PrepareKeyForDefaultCache(SettingsDefaults.SettingsCacheKey, typeof(T).Name));
        return setting;
    }

    public async Task Save(T setting)
    {
        if (setting.Id == 0)
            await _repository.InsertAsync(setting);
        else
            await _repository.UpdateAsync(setting);
    }

    #endregion
}
