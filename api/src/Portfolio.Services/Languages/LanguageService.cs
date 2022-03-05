using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Models.Common;
using Portfolio.Domain.Models.Localization;
using Portfolio.Services.Repository;

namespace Portfolio.Services.Languages;

public class LanguageService : ILanguageService
{
    #region Fields

    private readonly IBaseRepository<Language> _languageRepository;

    #endregion

    #region Constructor

    public LanguageService(IBaseRepository<Language> languageRepository)
    {
        _languageRepository = languageRepository;
    }

    #endregion

    #region Methods

    public async Task<IPagedList<Language>> GetAllAsync(bool onlyShowPublished, int pageIndex = 0, int pageSize = int.MaxValue)
    {
        return await _languageRepository.GetAllPagedAsync(query =>
        {
            if (onlyShowPublished)
                query = query.Where(language => language.IsPublished);

            return query;
        }, pageIndex, pageSize);
    }

    public Task<Language> GetByIdAsync(int languageId)
    {
        return _languageRepository.GetByIdAsync(languageId);
    }

    public Task<bool> IsExistingNameOrCulture(string name, string culture, int idToIgnore = 0)
    {
        return _languageRepository.Table.AnyAsync(language => (language.Name.ToLower() == name.ToLower() || language.LanguageCulture.ToLower() == culture.ToLower())
                && (idToIgnore == 0 || language.Id != idToIgnore));
    }

    public Task InsertAsync(Language language)
    {
        return _languageRepository.InsertAsync(language);
    }

    public Task UpdateAsync(Language language)
    {
        return _languageRepository.UpdateAsync(language);
    }

    public Task DeleteAsync(Language language)
    {
        return _languageRepository.DeleteAsync(language);
    }

    #endregion
}
