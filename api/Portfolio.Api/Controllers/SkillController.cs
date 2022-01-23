﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Core.Services.SkillGroups;
using Portfolio.Core.Services.Skills;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Dtos.Skills;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

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
        var skills = (await _skillService.GetAll()).ToListResult();

        var result = _mapper.Map<ListResult<SkillDto>>(skills);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("GetBySkillGroupId/{skillGroupId}")]
    public async Task<IActionResult> GetBySkillGroupId(int skillGroupId)
    {
        var skills = (await _skillService.GetBySkillGroupId(skillGroupId)).ToListResult();

        var result = _mapper.Map<ListResult<SkillDto>>(skills);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSkillDto model)
    {
        if (!ModelState.IsValid)
            return Ok(await Result.FailAsync("Invalid model"));

        if (!await _skillGroupService.Exists(model.SkillGroupId))
            return Ok(await Result.FailAsync("Skill group not found"));

        if (await _skillService.IsExistingSkill(model.Name, model.SkillGroupId))
            return Ok(await Result.FailAsync("There already is a skill with the same name"));

        var skill = _mapper.Map<Skill>(model);
        await _skillService.Insert(skill);

        var result = await Result<SkillDto>.SuccessAsync(_mapper.Map<SkillDto>(skill));
        return Ok(result);
    }

    [HttpPut("SaveSkillImage/{skillId}")]
    public async Task<IActionResult> SaveSkillImage(int skillId, IFormFile icon)
    {
        var skill = await _skillService.GetById(skillId);
        if (skill == null)
            return Ok(await Result.FailAsync("No skill found with the provided id"));

        var errorMessage = _uploadImageHelper.ValidateImage(icon);
        if (!string.IsNullOrEmpty(errorMessage))
            return Ok(await Result.FailAsync(errorMessage));

        skill.IconPath = await _uploadImageHelper.UploadImage(icon);
        await _skillService.Update(skill);

        var result = await Result<SkillDto>.SuccessAsync(_mapper.Map<SkillDto>(skill));
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateSkillDto model)
    {
        if (!ModelState.IsValid)
            return Ok(await Result.FailAsync("Invalid model"));

        var skill = await _skillService.GetById(model.Id);
        if (skill == null)
            return Ok(await Result.FailAsync("Skill not found"));

        if (!await _skillGroupService.Exists(model.SkillGroupId))
            return Ok(await Result.FailAsync("Skill group not found"));

        if (await _skillService.IsExistingSkill(model.Name, model.SkillGroupId, skill))
            return Ok(await Result.FailAsync("There already is a skill with the same name"));

        model.IconPath = skill.IconPath;

        _mapper.Map(model, skill);
        await _skillService.Update(skill);

        var result = await Result<SkillDto>.SuccessAsync(_mapper.Map<SkillDto>(skill));
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _skillService.Exists(id))
            return Ok(await Result.FailAsync("Skill not found"));

        await _skillService.Delete(id);

        return Ok(await Result.SuccessAsync("Removed the skill"));
    }

    #endregion

}
