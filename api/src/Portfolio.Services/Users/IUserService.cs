using Microsoft.AspNetCore.Identity;
using Portfolio.Domain.Models.Authentication;

namespace Portfolio.Services.Users;

public interface IUserService
{
    Task<ApplicationUser> GetUserByName(string userName);

    Task<ApplicationUser> GetUserByNameOrEmail(string userName);

    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

    Task<IdentityResult> UpdateAsync(ApplicationUser user);

    Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPaswword, string newPassword);
}
