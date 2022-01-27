using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Dtos.Skills;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;
using Portfolio.Services.Common;
using Portfolio.Services.SkillGroups;
using Portfolio.Services.Skills;
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
        var skills = (await _skillService.GetAllAsync()).ToListResult();

        var result = _mapper.Map<ListResult<SkillDto>>(skills);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("GetBySkillGroupId/{skillGroupId}")]
    public async Task<IActionResult> GetBySkillGroupId(int skillGroupId)
    {
        var skills = (await _skillService.GetBySkillGroupIdAsync(skillGroupId)).ToListResult();

        var result = _mapper.Map<ListResult<SkillDto>>(skills);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSkillDto model)
    {
        if (!ModelState.IsValid)
            return Ok(await Result.FailAsync(ModelState.Select(x => x.Value.Errors.ToString()).FirstOrDefault()));

        if (!await _skillGroupService.ExistsAsync(model.SkillGroupId))
            return Ok(await Result.FailAsync("Skill group not found"));

        if (await _skillService.IsExistingSkillAsync(model.Name, model.SkillGroupId))
            return Ok(await Result.FailAsync("There already is a skill with the same name"));

        var skill = _mapper.Map<Skill>(model);
        await _skillService.InsertAsync(skill);

        var result = await Result<SkillDto>.SuccessAsync(_mapper.Map<SkillDto>(skill));
        return Ok(result);
    }

    [HttpPut("SaveSkillImage/{skillId}")]
    public async Task<IActionResult> SaveSkillImage(int skillId, IFormFile icon)
    {
        var skill = await _skillService.GetByIdAsync(skillId);
        if (skill == null)
            return Ok(await Result.FailAsync("No skill found with the provided id"));

        var errorMessage = _uploadImageHelper.ValidateImage(icon);
        if (!string.IsNullOrEmpty(errorMessage))
            return Ok(await Result.FailAsync(errorMessage));

        skill.IconPath = await _uploadImageHelper.UploadImageAsync(icon);
        await _skillService.UpdateAsync(skill);

        var result = await Result<SkillDto>.SuccessAsync(_mapper.Map<SkillDto>(skill));
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateSkillDto model)
    {
        if (!ModelState.IsValid)
            return Ok(await Result.FailAsync("Invalid model"));

        var skill = await _skillService.GetByIdAsync(model.Id);
        if (skill == null)
            return Ok(await Result.FailAsync("Skill not found"));

        if (!await _skillGroupService.ExistsAsync(model.SkillGroupId))
            return Ok(await Result.FailAsync("Skill group not found"));

        if (await _skillService.IsExistingSkillAsync(model.Name, model.SkillGroupId, skill))
            return Ok(await Result.FailAsync("There already is a skill with the same name"));

        model.IconPath = skill.IconPath;

        _mapper.Map(model, skill);
        await _skillService.UpdateAsync(skill);

        var result = await Result<SkillDto>.SuccessAsync(_mapper.Map<SkillDto>(skill));
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _skillService.ExistsAsync(id))
            return Ok(await Result.FailAsync("Skill not found"));

        await _skillService.DeleteAsync(id);

        return Ok(await Result.SuccessAsync("Removed the skill"));
    }

    #endregion

}
