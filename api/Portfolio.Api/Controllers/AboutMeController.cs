using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Core.Interfaces;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Models;

namespace Portfolio.Controllers
{
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

        #region Methods

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var aboutMe = await _aboutMeService.GetAboutMe();
            if (aboutMe == null)
                aboutMe = new AboutMe();

            return Ok(_mapper.Map<AboutMeDto>(aboutMe));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Save(AboutMeDto model)
        {
            var aboutMe = _mapper.Map<AboutMe>(model);
            await _aboutMeService.SaveAboutMe(aboutMe);

            return Ok(_mapper.Map<AboutMeDto>(aboutMe));
        }

        #endregion
    }
}
