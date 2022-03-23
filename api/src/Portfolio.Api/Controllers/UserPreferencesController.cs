using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Dtos.Authentication;
using Portfolio.Domain.Dtos.UserPreference;
using Portfolio.Domain.Wrapper;
using Portfolio.Services.Languages;
using Portfolio.Services.UserPreference;
using Portfolio.Services.WorkContexts;
using System;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserPreferencesController : ControllerBase
{
    #region Fields

    private readonly IWorkContext _workContext;
    private readonly ILanguageService _languageService;
    private readonly IUserPreferencesService _userPreferencesService;
    private readonly IMapper _mapper;

    #endregion

    #region Constructors

    public UserPreferencesController(IWorkContext workContext, ILanguageService languageService, IUserPreferencesService userPreferencesService, IMapper mapper)
    {
        _workContext = workContext;
        _languageService = languageService;
        _userPreferencesService = userPreferencesService;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    #region Post

    [HttpPost]
    [Route("UpdateSelectedLanguage")]
    public async Task<IActionResult> UpdateSelectedLanguage(UpdateLanguageDto updateDto)
    {
        var currentUser = await _workContext.GetCurrentUserAsync();
        if (currentUser == null)
            throw new ApplicationException("No logged in user found");

        var language = await _languageService.GetByIdAsync(updateDto.languageId);
        if (language == null)
            return Ok(await Result.FailAsync("Requested language not found"));

        var preferences = await _userPreferencesService.UpdateSelectedLanguage(currentUser, language);

        var dto = _mapper.Map<UserPreferencesDto>(preferences);

        return Ok(await Result<UserPreferencesDto>.SuccessAsync(dto));
    }

    [HttpPost]
    [Route("UpdateIsUseDarkMode")]
    public async Task<IActionResult> UpdateIsUseDarkMode(UpdateIsUseDarkModeDto updateDto)
    {
        var currentUser = await _workContext.GetCurrentUserAsync();
        if (currentUser == null)
            throw new ApplicationException("No logged in user found");

        var preferences = await _userPreferencesService.UpdateIsUseDarkMode(currentUser, updateDto.IsUseDarkMode);

        var dto = _mapper.Map<UserPreferencesDto>(preferences);
        return Ok(await Result<UserPreferencesDto>.SuccessAsync(dto));
    }

    #endregion

    #endregion
}
