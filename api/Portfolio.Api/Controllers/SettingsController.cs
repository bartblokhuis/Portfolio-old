﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Core.Services.Settings;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Models.Settings;
using Portfolio.Domain.Wrapper;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize()]
public class SettingsController : Controller
{
    #region Fields

    private readonly ILogger<SettingsController> _logger;
    private readonly ISettingService<EmailSettings> _emailSettingsService;
    private readonly ISettingService<GeneralSettings> _generalSettings;
    private readonly ISettingService<SeoSettings> _seoSettings;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public SettingsController(ILogger<SettingsController> logger, ISettingService<EmailSettings> emailSettingsService, ISettingService<GeneralSettings> generalSettings, ISettingService<SeoSettings> seoSettings, IEmailService emailService, IMapper mapper)
    {
        _logger = logger;
        _emailSettingsService = emailSettingsService;
        _generalSettings = generalSettings;
        _seoSettings = seoSettings;
        _emailService = emailService;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    #region GeneralSettings

    [HttpGet("GeneralSettings")]
    [AllowAnonymous]
    public async Task<IActionResult> GetGeneralSettings()
    {
        var settings = await _generalSettings.Get();

        var dto = _mapper.Map<GeneralSettingsDto>(settings);
        dto ??= new GeneralSettingsDto();

        var result = await Result<GeneralSettingsDto>.SuccessAsync(dto);
        return Ok(result);
    }

    [HttpPost("GeneralSettings")]
    public async Task<IActionResult> SaveGeneralSettings(GeneralSettingsDto model)
    {
        var originalSettings = await _generalSettings.Get();

        originalSettings ??= new GeneralSettings();
        _mapper.Map(model, originalSettings);

        await _generalSettings.Save(originalSettings);

        var result = await Result<GeneralSettingsDto>.SuccessAsync(_mapper.Map<GeneralSettingsDto>(originalSettings));
        return Ok(result);
    }

    #endregion

    #region EmailSettings

    [HttpGet("EmailSettings")]
    public async Task<IActionResult> GetEmailSettings()
    {
        var settings = await _emailSettingsService.Get();

        //We don't want to send the password in the get method.
        if (settings != null)
            settings.Password = string.Empty;

        var dto = _mapper.Map<EmailSettingsDto>(settings);
        dto ??= new EmailSettingsDto();

        var result = await Result<EmailSettingsDto>.SuccessAsync(dto);
        return Ok(result);
    }

    [HttpPost("EmailSettings")]
    public async Task<IActionResult> SaveEmailSettings(EmailSettingsDto model)
    {
        //Test the new configuration before saving it.
        if (!await _emailService.SendEmail(
            new MailboxAddress(model.DisplayName, model.SendTestEmailTo), 
            "Test email",
            new TextPart("plain") { Text = "Test email" }, 
            _mapper.Map<EmailSettings>(model)))
        {
            return Ok(await Result.FailAsync("Test email failed"));
        }


        var originalSettings = await _emailSettingsService.Get();

        originalSettings ??= new EmailSettings();
        _mapper.Map(model, originalSettings);

        await _emailSettingsService.Save(originalSettings);

        var result = await Result<EmailSettingsDto>.SuccessAsync(_mapper.Map<EmailSettingsDto>(originalSettings));
        return Ok(result);
    }

    #endregion

    #region SeoSettings

    [HttpGet("SeoSettings")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSeoSettings()
    {
        var settings = await _seoSettings.Get();

        var dto = _mapper.Map<SeoSettingsDto>(settings);
        dto ??= new SeoSettingsDto();

        var result = await Result<SeoSettingsDto>.SuccessAsync(dto);
        return Ok(result);
    }

    [HttpPost("SeoSettings")]
    public async Task<IActionResult> SaveSeoSettings(SeoSettingsDto model)
    {
        var originalSettings = await _seoSettings.Get();

        originalSettings ??= new SeoSettings();
        _mapper.Map(model, originalSettings);

        await _seoSettings.Save(originalSettings);

        var result = await Result<SeoSettingsDto>.SuccessAsync(_mapper.Map<SeoSettingsDto>(originalSettings));
        return Ok(result);
    }

    #endregion

    #endregion

}

