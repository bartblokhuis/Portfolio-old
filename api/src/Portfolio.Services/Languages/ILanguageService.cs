using Portfolio.Domain.Models.Common;
using Portfolio.Domain.Models.Localization;

namespace Portfolio.Services.Languages;

public interface ILanguageService
{
    Task<IPagedList<Language>> GetAllAsync(int pageIndex = 0, int pageSize = int.MaxValue);

    Task<Language> GetByIdAsync(int languageId);

    Task<bool> IsExistingNameOrCulture(string name, string culture, int idToIgnore = 0);

    Task InsertAsync(Language language);

    Task UpdateAsync(Language language);

    Task DeleteAsync(Language language);
}
