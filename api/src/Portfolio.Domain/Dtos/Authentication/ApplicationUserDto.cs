using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain.Dtos.Authentication;

public class ApplicationUserDto
{
    #region Properties

    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    public UserPreferencesDto UserPreferences { get; set; }

    #endregion
}
