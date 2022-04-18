namespace Portfolio.Domain.Dtos.Authentication;

public class UserPreferencesDto
{
    #region Properties

    public bool? IsUseDarkMode { get; set; }

    public int? SelectedLanguageId { get; set; }

    public string ApplicationUserId { get; set; }

    #endregion
}
