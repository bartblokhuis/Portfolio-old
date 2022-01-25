using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Helpers;
using Portfolio.Services.Urls;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UrlController : ControllerBase
{
    #region Fields

    private readonly IUrlService _urlService;

    #endregion

    #region Constructor

    public UrlController(IUrlService urlService)
    {
        _urlService = urlService;
    }

    #endregion

    #region Methods

    [HttpPut]
    public async Task<IActionResult> Update(Url url)
    {
        if(string.IsNullOrEmpty(url.FullUrl))
            return Ok(await Result.FailAsync($"Please enter a url"));

        if (!CommonHelper.IsValidUrl(url.FullUrl))
            return Ok(await Result.FailAsync($"Please use a real url"));

        await _urlService.UpdateAsync(url);

        var result = await Result<Url>.SuccessAsync(url);
        return Ok(result);
    }

    #endregion
}