using System;

namespace Portfolio.Domain.Dtos.Authentication;

public class LoginResultDto
{
    public string Token { get; set; }

    public DateTime Expiration { get; set; }

    public ApplicationUserDto User { get; set; }
}
