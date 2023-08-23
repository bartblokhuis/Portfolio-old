using Portfolio.Database;
using Portfolio.Domain.Models.Authentication;
using Portfolio.Domain.Models.Localization;
using Portfolio.Services.Repository;

namespace Portfolio.Services.UserPreference;

public class UserPreferencesService : IUserPreferencesService
{
    #region Fields

    private readonly IBaseRepository<UserPreferences, int, AuthenticationDbContext> _userPreferenceRepo;

    #endregion

    #region Constructor

    public UserPreferencesService(IBaseRepository<UserPreferences, int, AuthenticationDbContext> userPreferenceRepo)
    {
        _userPreferenceRepo = userPreferenceRepo;
    }

    #endregion

    #region Utils

    private async Task<UserPreferences> GetOrCreateUserPreferences(ApplicationUser user)
    {
        var preferences = await _userPreferenceRepo.FirstOrDefaultAsync(x => x.ApplicationUserId == user.Id);
        if(preferences != null) 
            return preferences;

        preferences = new UserPreferences
        {
            ApplicationUserId = user.Id
        };

        await _userPreferenceRepo.InsertAsync(preferences);
        return preferences;
    }

    #endregion

    #region Methods

    public async Task<UserPreferences> UpdateSelectedLanguage(ApplicationUser user, Language language)
    {
        var userPreferences = await GetOrCreateUserPreferences(user);

        userPreferences.SelectedLanguageId = language.Id;
        await _userPreferenceRepo.UpdateAsync(userPreferences);

        return userPreferences;
    }

    public async Task<UserPreferences> UpdateIsUseDarkMode(ApplicationUser user, bool isUseDarkMode)
    {
        var userPreferences = await GetOrCreateUserPreferences(user);

        userPreferences.IsUseDarkMode = isUseDarkMode;
        await _userPreferenceRepo.UpdateAsync(userPreferences);

        return userPreferences;
    }

    #endregion
}
