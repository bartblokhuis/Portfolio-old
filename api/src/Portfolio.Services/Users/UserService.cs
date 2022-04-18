using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Domain.Models.Authentication;
using Portfolio.Services.Repository;

namespace Portfolio.Services.Users;

public class UserService : IUserService
{
    #region Fields

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBaseRepository<ApplicationUser, string, AuthenticationDbContext> _userRepository;

    #endregion

    #region Constructor

    public UserService(UserManager<ApplicationUser> userManager, IBaseRepository<ApplicationUser, string, AuthenticationDbContext> userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    #endregion

    #region Methods

    public async Task<ApplicationUser?> GetUserByName(string userName)
    {
        var users = await _userRepository.GetAllAsync(query =>
        {
            query = query.Where(x => x.UserName.ToLower() == userName.ToLower());
            query = query.Include(x => x.UserPreferences);

            return query;
        });

        return users?.FirstOrDefault();
    }

    public async Task<ApplicationUser?> GetUserByNameOrEmail(string userName)
    {
        var users = await _userRepository.GetAllAsync(query =>
        {
            query = query.Where(x => x.UserName.ToLower() == userName.ToLower() || x.NormalizedEmail == userName.ToUpper());
            query = query.Include(x => x.UserPreferences);

            return query;
        });

        return users?.FirstOrDefault();
    }

    public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
        return _userManager.CheckPasswordAsync(user, password);
    }

    public Task<IdentityResult> UpdateAsync(ApplicationUser user)
    {
        return _userManager.UpdateAsync(user);
    }

    public Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPaswword, string newPassword)
    {
        return _userManager.ChangePasswordAsync(user, currentPaswword, newPassword);
    }

    #endregion
}
