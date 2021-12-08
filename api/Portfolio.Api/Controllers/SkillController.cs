using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Dtos.Skills;
using Portfolio.Domain.Models;

namespace Portfolio.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SkillController : ControllerBase
    {
        #region Fields

        private readonly ISkillService _skillService;
        private readonly ISkillGroupService _skillGroupService;
        private readonly IUploadImageHelper _uploadImageHelper;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public SkillController(ISkillService skillService, ISkillGroupService skillGroupService, IUploadImageHelper uploadImageHelper, IMapper mapper)
        {
            _skillService = skillService;
            _skillGroupService = skillGroupService;
            _uploadImageHelper = uploadImageHelper;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var skills = await _skillService.GetAll();
            return Ok(_mapper.Map<IEnumerable<SkillDto>>(skills));
        }

        [AllowAnonymous]
        [HttpGet("GetBySkillGroupId/{skillGroupId}")]
        public async Task<IActionResult> GetBySkillGroupId(int skillGroupId)
        {
            var skills = await _skillService.GetBySkillGroupId(skillGroupId);
            return Ok(_mapper.Map<IEnumerable<SkillDto>>(skills));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSkillDto model)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model"); //TODO Better excaption handling

            if (!await _skillGroupService.Exists(model.SkillGroupId))
                throw new Exception("Skill group not found");

            if (await _skillService.IsExistingSkill(model.Name, model.SkillGroupId))
                throw new Exception("There already is a skill with the same name");

            var skill = _mapper.Map<Skill>(model);
            await _skillService.Insert(skill);

            return Ok(_mapper.Map<SkillDto>(skill));
        }

        [HttpPut("SaveSkillImage/{skillId}")]
        public async Task<IActionResult> SaveSkillImage(int skillId, IFormFile icon)
        {
            var skill = await _skillService.GetById(skillId);
            if (skill == null)
                throw new Exception("No skill found with the provided id");

            var errorMessage = _uploadImageHelper.ValidateImage(icon);
            if (!string.IsNullOrEmpty(errorMessage))
                throw new Exception(errorMessage);

            skill.IconPath = await _uploadImageHelper.UploadImage(icon);
            await _skillService.Update(skill);
            return Ok(_mapper.Map<SkillDto>(skill));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateSkillDto model)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model"); //TODO Better excaption handling

            var skill = await _skillService.GetById(model.Id);
            if (skill == null)
                throw new Exception("Skill not found");

            if (!await _skillGroupService.Exists(model.SkillGroupId))
                throw new Exception("Skill group not found");

            if (await _skillService.IsExistingSkill(model.Name, model.SkillGroupId, skill))
                throw new Exception("There already is a skill with the same name");

            model.IconPath = skill.IconPath;

            _mapper.Map(model, skill);
            await _skillService.Update(skill);

            return Ok(_mapper.Map<SkillDto>(skill));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _skillService.Exists(id))
                return BadRequest("Skill not found");

            await _skillService.Delete(id);

            return Ok();
        }

        #endregion

    }
}
