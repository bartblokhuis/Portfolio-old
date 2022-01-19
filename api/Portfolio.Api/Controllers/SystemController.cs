using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Caching;
using Portfolio.Domain.Wrapper;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class SystemController : ControllerBase
{
    #region Fields

    private readonly IStaticCacheManager _cacheManager;

    #endregion

    #region Constructor

    public SystemController(IStaticCacheManager cacheManager)
    {
        _cacheManager = cacheManager;
    }

    #endregion

    #region Methods

    [HttpGet("ClearCache")]
    public async Task<IActionResult> ClearCache()
    {
        await _cacheManager.ClearAsync();

        var result = await Result.SuccessAsync("Cleared the system cache");
        return Ok(result);
    }

    #endregion
}

