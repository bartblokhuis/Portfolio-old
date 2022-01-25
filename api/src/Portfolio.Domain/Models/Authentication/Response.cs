using System;

namespace Portfolio.Domain.Models.Authentication;

public class Response
{

    #region Constructor

    public Response(string token, DateTime expiration, string userId)
    {
        Token = token;
        Expiration = expiration;
        UserId = userId;
    }

    #endregion

    #region Properties

    public string Token { get; set; }

    public DateTime Expiration { get; set; }

    public string UserId { get; set; }

    #endregion

}
