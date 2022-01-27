using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;
using Portfolio.Services.AboutMeServices;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class AboutMeController : ControllerBase
{
    #region Fields

    private readonly ILogger<AboutMeController> _logger;
    private readonly IAboutMeService _aboutMeService;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public AboutMeController(ILogger<AboutMeController> logger, IAboutMeService aboutMeService, IMapper mapper)
    {
        _logger = logger;
        _aboutMeService = aboutMeService;
        _mapper = mapper;
    }

    #endregion

    #region Utils

    private string Validate(AboutMeDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title))
            return "Please enter a title";

        if (dto.Title.Length > 128)
            return "Please don't use more than 128 character in the title";

        return "";
    }

    #endregion

    #region Methods

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var aboutMe = await _aboutMeService.GetAsync();
        if (aboutMe == null)
            aboutMe = new AboutMe();

        var result = await Result<AboutMeDto>.SuccessAsync(_mapper.Map<AboutMeDto>(aboutMe));
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Save(AboutMeDto model)
    {
        var error = Validate(model);
        if (!string.IsNullOrEmpty(error))
            return Ok(await Result.FailAsync(error));

        var aboutMe = _mapper.Map<AboutMe>(model);
        await _aboutMeService.SaveAsync(aboutMe);

        var result = await Result<AboutMeDto>.SuccessAsync(_mapper.Map<AboutMeDto>(aboutMe));
        return Ok(result);
    }

    #endregion
}

