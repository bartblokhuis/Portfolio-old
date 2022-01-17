using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Core.Interfaces;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Dtos.SkillGroup;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class SkillGroupController : ControllerBase
{
    #region Fields

    private ILogger<SkillGroupController> _logger;
    private readonly ISkillGroupService _skillGroupService;
    private readonly ISkillService _skillService;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public SkillGroupController(ILogger<SkillGroupController> logger, ISkillGroupService skillGroupService, ISkillService skillService, IMapper mapper)
    {
        _logger = logger;
        _skillGroupService = skillGroupService;
        _skillService = skillService;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var skillGroups = (await _skillGroupService.GetAll()).ToListResult();

        if(skillGroups.Data != null)
            foreach (var skill in skillGroups.Data.SelectMany(x => x.Skills))
                skill.SkillGroup = null;

        var result = _mapper.Map<ListResult<SkillGroupDto>>(skillGroups);
        result.Succeeded = true;
        return Ok(result);
    }
 
    [HttpPost]
    public async Task<IActionResult> Create(CreateUpdateSkillGroupDto model)
    {
        if (!ModelState.IsValid)
            throw new Exception("Invalid model");

        if (await _skillGroupService.IsExistingTitle(model.Title))
            return Ok(await Result.FailAsync("There is already a skill group with the same title"));

        var skillGroup = _mapper.Map<SkillGroup>(model);
        await _skillGroupService.Insert(skillGroup);

        var result = await Result<SkillGroupDto>.SuccessAsync(_mapper.Map<SkillGroupDto>(skillGroup));
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CreateUpdateSkillGroupDto model)
    {
        if (!ModelState.IsValid)
            throw new Exception("Invalid model");

        if (!await _skillGroupService.Exists(model.Id))
            return Ok(await Result.FailAsync($"No skill group for id: {model.Id} found"));

        if (await _skillGroupService.IsExistingTitle(model.Title, model.Id))
            return Ok(await Result.FailAsync("There is already a skill group with the same title"));

        var skillGroup = _mapper.Map<SkillGroup>(model);
        await _skillGroupService.Update(skillGroup);
        
        skillGroup.Skills = (await _skillService.GetBySkillGroupId(skillGroup.Id)).ToList();

        var result = await Result<SkillGroupDto>.SuccessAsync(_mapper.Map<SkillGroupDto>(skillGroup));
        return Ok(result);
    }
        
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var skillGroup = await _skillGroupService.GetById(id);
        if (skillGroup == null)
            return Ok(await Result.FailAsync("Skill group not found"));

        await _skillGroupService.Delete(skillGroup);

        var result = await Result.SuccessAsync("Removed the skill group");
        return Ok(result);
    }

    #endregion

}
