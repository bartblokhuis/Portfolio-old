using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain.Dtos.Authentication;

public class UpdateUserDetailsDto
{
    #region Properties

    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    #endregion
}
