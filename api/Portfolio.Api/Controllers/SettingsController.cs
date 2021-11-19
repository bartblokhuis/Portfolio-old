using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Models;
using System.Threading.Tasks;

namespace Portfolio.Controllers
{
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
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public SettingsController(ILogger<SettingsController> logger, ISettingService<EmailSettings> emailSettingsService, ISettingService<GeneralSettings> generalSettings, ISettingService<SeoSettings> seoSettings, IMapper mapper)
        {
            _logger = logger;
            _emailSettingsService = emailSettingsService;
            _generalSettings = generalSettings;
            _seoSettings = seoSettings;
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

            return Ok(dto);
        }

        [HttpPost("GeneralSettings")]
        public async Task<IActionResult> SaveGeneralSettings(GeneralSettingsDto model)
        {
            var originalSettings = await _generalSettings.Get();

            originalSettings ??= new GeneralSettings();
            _mapper.Map(model, originalSettings);

            await _generalSettings.Save(originalSettings);

            return Ok(_mapper.Map<GeneralSettingsDto>(originalSettings));
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

            return Ok(dto);
        }

        [HttpPost("EmailSettings")]
        public async Task<IActionResult> SaveEmailSettings(EmailSettingsDto model)
        {
            var originalSettings = await _emailSettingsService.Get();

            originalSettings ??= new EmailSettings();
            _mapper.Map(model, originalSettings);

            await _emailSettingsService.Save(originalSettings);

            return Ok(_mapper.Map<EmailSettingsDto>(originalSettings));
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

            return Ok(dto);
        }

        [HttpPost("SeoSettings")]
        public async Task<IActionResult> SaveSeoSettings(SeoSettingsDto model)
        {
            var originalSettings = await _seoSettings.Get();

            originalSettings ??= new SeoSettings();
            _mapper.Map(model, originalSettings);

            await _seoSettings.Save(originalSettings);

            return Ok(_mapper.Map<SeoSettingsDto>(originalSettings));
        }

        #endregion

        #endregion

    }
}
