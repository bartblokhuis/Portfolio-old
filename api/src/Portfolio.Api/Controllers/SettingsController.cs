﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Services.Common;
using Portfolio.Services.Settings;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Dtos.Settings;
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
    private readonly ISettingService<BlogSettings> _blogSettings;
    private readonly ISettingService<PublicSiteSettings> _publicSiteSettings;
    private readonly ISettingService<ApiSettings> _apiSettings;
    private readonly ISettingService<MessageSettings> _messageSettings;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public SettingsController(ILogger<SettingsController> logger, ISettingService<EmailSettings> emailSettingsService, ISettingService<GeneralSettings> generalSettings, ISettingService<SeoSettings> seoSettings, ISettingService<BlogSettings> blogSettings, ISettingService<PublicSiteSettings> publicSiteSettings, IEmailService emailService, IMapper mapper, ISettingService<ApiSettings> apiSettings, ISettingService<MessageSettings> messageSettings)
    {
        _logger = logger;
        _emailSettingsService = emailSettingsService;
        _generalSettings = generalSettings;
        _seoSettings = seoSettings;
        _blogSettings = blogSettings;
        _publicSiteSettings = publicSiteSettings;
        _emailService = emailService;
        _mapper = mapper;
        _apiSettings = apiSettings;
        _messageSettings = messageSettings;
    }

    #endregion

    #region Methods

    #region GeneralSettings

    [HttpGet("GeneralSettings")]
    [AllowAnonymous]
    public async Task<IActionResult> GetGeneralSettings()
    {
        var settings = await _generalSettings.GetAsync();

        var dto = _mapper.Map<GeneralSettingsDto>(settings);
        dto ??= new GeneralSettingsDto();

        var result = await Result<GeneralSettingsDto>.SuccessAsync(dto);
        return Ok(result);
    }

    [HttpPost("GeneralSettings")]
    public async Task<IActionResult> SaveGeneralSettings(GeneralSettingsDto model)
    {
        var originalSettings = await _generalSettings.GetAsync();

        originalSettings ??= new GeneralSettings();
        _mapper.Map(model, originalSettings);

        await _generalSettings.SaveAsync(originalSettings);

        var result = await Result<GeneralSettingsDto>.SuccessAsync(_mapper.Map<GeneralSettingsDto>(originalSettings));
        return Ok(result);
    }

    #endregion

    #region EmailSettings

    [HttpGet("EmailSettings")]
    public async Task<IActionResult> GetEmailSettings()
    {
        var settings = await _emailSettingsService.GetAsync();

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
        if (!await _emailService.SendEmail(model.DisplayName, model.SendTestEmailTo, 
            "Test email",
            "Test email",
            _mapper.Map<EmailSettings>(model)))
        {
            return Ok(await Result.FailAsync("Test email failed"));
        }


        var originalSettings = await _emailSettingsService.GetAsync();

        originalSettings ??= new EmailSettings();
        _mapper.Map(model, originalSettings);

        await _emailSettingsService.SaveAsync(originalSettings);

        var result = await Result<EmailSettingsDto>.SuccessAsync(_mapper.Map<EmailSettingsDto>(originalSettings));
        return Ok(result);
    }

    #endregion

    #region SeoSettings

    [HttpGet("SeoSettings")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSeoSettings()
    {
        var settings = await _seoSettings.GetAsync();

        var dto = _mapper.Map<SeoSettingsDto>(settings);
        dto ??= new SeoSettingsDto();

        var result = await Result<SeoSettingsDto>.SuccessAsync(dto);
        return Ok(result);
    }

    [HttpPost("SeoSettings")]
    public async Task<IActionResult> SaveSeoSettings(SeoSettingsDto model)
    {
        var originalSettings = await _seoSettings.GetAsync();

        originalSettings ??= new SeoSettings();
        _mapper.Map(model, originalSettings);

        await _seoSettings.SaveAsync(originalSettings);

        var result = await Result<SeoSettingsDto>.SuccessAsync(_mapper.Map<SeoSettingsDto>(originalSettings));
        return Ok(result);
    }

    #endregion

    #region BlogSettings

    [HttpGet("BlogSettings")]
    [AllowAnonymous]
    public async Task<IActionResult> GetBlogSettings()
    {
        var settings = await _blogSettings.GetAsync();

        var dto = _mapper.Map<BlogSettingsDto>(settings);
        dto ??= new BlogSettingsDto();

        var result = await Result<BlogSettingsDto>.SuccessAsync(dto);
        return Ok(result);
    }

    [HttpPost("BlogSettings")]
    public async Task<IActionResult> SaveBlogSettings(BlogSettingsDto model)
    {
        var originalSettings = await _blogSettings.GetAsync();

        originalSettings ??= new BlogSettings();
        _mapper.Map(model, originalSettings);

        await _blogSettings.SaveAsync(originalSettings);

        var result = await Result<BlogSettingsDto>.SuccessAsync(_mapper.Map<BlogSettingsDto>(originalSettings));
        return Ok(result);
    }

    #endregion

    #region PublicSiteSettings

    [HttpGet("PublicSiteSettings")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPublicSiteSettings()
    {
        var settings = await _publicSiteSettings.GetAsync();

        var dto = _mapper.Map<PublicSiteSettingsDto>(settings);
        dto ??= new PublicSiteSettingsDto();

        var result = await Result<PublicSiteSettingsDto>.SuccessAsync(dto);
        return Ok(result);
    }

    [HttpPost("PublicSiteSettings")]
    public async Task<IActionResult> SavePublicSiteSettings(PublicSiteSettingsDto model)
    {
        var originalSettings = await _publicSiteSettings.GetAsync();

        originalSettings ??= new PublicSiteSettings();
        _mapper.Map(model, originalSettings);

        await _publicSiteSettings.SaveAsync(originalSettings);

        var result = await Result<PublicSiteSettingsDto>.SuccessAsync(_mapper.Map<PublicSiteSettingsDto>(originalSettings));
        return Ok(result);
    }

    #endregion

    #region ApiSettings

    [HttpGet("ApiSettings")]
    [AllowAnonymous]
    public async Task<IActionResult> GetApiSettings()
    {
        var settings = await _apiSettings.GetAsync();

        var dto = _mapper.Map<ApiSettingsDto>(settings);
        dto ??= new ApiSettingsDto();

        var result = await Result<ApiSettingsDto>.SuccessAsync(dto);
        return Ok(result);
    }

    [HttpPost("ApiSettings")]
    public async Task<IActionResult> SaveApiSettings(ApiSettingsDto model)
    {
        var originalSettings = await _apiSettings.GetAsync();

        originalSettings ??= new ApiSettings();
        _mapper.Map(model, originalSettings);

        await _apiSettings.SaveAsync(originalSettings);

        var result = await Result<ApiSettingsDto>.SuccessAsync(_mapper.Map<ApiSettingsDto>(originalSettings));
        return Ok(result);
    }

    #endregion

    #region Message Settings

    [HttpGet("MessageSettings")]
    [AllowAnonymous]
    public async Task<IActionResult> GetMessageSettings()
    {
        var settings = await _messageSettings.GetAsync();

        var dto = _mapper.Map<MessageSettingsDto>(settings);
        dto ??= new MessageSettingsDto();

        var result = await Result<MessageSettingsDto>.SuccessAsync(dto);
        return Ok(result);
    }

    [HttpPost("MessageSettings")]
    public async Task<IActionResult> SaveMessageSettings(MessageSettingsDto model)
    {
        var originalSettings = await _messageSettings.GetAsync();

        originalSettings ??= new MessageSettings();
        _mapper.Map(model, originalSettings);

        await _messageSettings.SaveAsync(originalSettings);

        var result = await Result<MessageSettingsDto>.SuccessAsync(_mapper.Map<MessageSettingsDto>(originalSettings));
        return Ok(result);
    }

    #endregion

    #endregion

}
