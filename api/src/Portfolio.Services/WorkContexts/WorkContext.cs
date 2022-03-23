using Microsoft.AspNetCore.Http;
using Portfolio.Domain.Models.Authentication;
using Portfolio.Services.Users;
using System.Security.Claims;

namespace Portfolio.Services.WorkContexts;

public class WorkContext : IWorkContext
{
    #region Fields

    private readonly IUserService _userService; 
    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion

    #region Constructor

    public WorkContext(IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    #endregion

    #region Utils

    #endregion

    #region Methods

    public async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        var currentUserName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(currentUserName))
            return null;

        return await _userService.GetUserByName(currentUserName);
    }

    #endregion
}
