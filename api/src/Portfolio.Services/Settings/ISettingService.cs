using Portfolio.Domain.Models.Common;

namespace Portfolio.Services.Settings;

public interface ISettingService<T> where T : BaseEntity, ISetting
{
    Task<T> GetAsync();

    Task SaveAsync(T settings);
}
