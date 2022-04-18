using Portfolio.Domain.Models.Authentication;

namespace Portfolio.Services.WorkContexts;

public interface IWorkContext
{
    Task<ApplicationUser?> GetCurrentUserAsync();
}
