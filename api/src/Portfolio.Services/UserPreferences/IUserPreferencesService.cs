using Portfolio.Domain.Models.Authentication;
using Portfolio.Domain.Models.Localization;

namespace Portfolio.Services.UserPreference;

public interface IUserPreferencesService
{
    Task<UserPreferences> UpdateSelectedLanguage(ApplicationUser user, Language language);

    Task<UserPreferences> UpdateIsUseDarkMode(ApplicationUser user, bool isUseDarkMode);
}
