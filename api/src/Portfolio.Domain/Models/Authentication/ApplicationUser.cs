using Microsoft.AspNetCore.Identity;
using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models.Authentication;

public class ApplicationUser : IdentityUser, IBaseEntity<string>
{
    #region Properties

    public UserPreferences UserPreferences { get; set; }

    #endregion
}
