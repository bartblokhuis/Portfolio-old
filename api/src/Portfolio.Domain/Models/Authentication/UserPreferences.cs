using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models.Authentication;

public class UserPreferences : BaseEntity
{
    #region Properties

    public bool? IsUseDarkMode { get; set; }

    public int? SelectedLanguageId { get; set; }

    public string ApplicationUserId { get; set; }

    public ApplicationUser ApplicationUser { get; set; }

    #endregion
}
