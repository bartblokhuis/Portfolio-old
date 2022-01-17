using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Dtos.Projects;
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
public class ProjectController : ControllerBase
{
    #region Fields

    private readonly ILogger<ProjectController> _logger;
    private readonly IProjectService _projectService;
    private readonly ISkillService _skillService;
    private readonly IMapper _mapper;
    private readonly IUploadImageHelper _uploadImageHelper;

    #endregion

    #region Constructor

    public ProjectController(ILogger<ProjectController> logger, IProjectService projectService, IMapper mapper, ISkillService skillService, IUploadImageHelper uploadImageHelper)
    {
        _logger = logger;
        _projectService = projectService;
        _mapper = mapper;
        _skillService = skillService;
        _uploadImageHelper = uploadImageHelper;
    }

    #endregion

    #region Methods

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var projects = (await _projectService.Get()).ToListResult();

        //Prevent infinit loop issues with the json serializer.
        if(projects.Data != null)
            foreach (var skill in projects.Data.SelectMany(x => x.Skills))
                skill.Projects = null;

        var result = _mapper.Map<ListResult<ProjectDto>>(projects);
        result.Succeeded = true;
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUpdateProject model)
    {
        var project = _mapper.Map<Project>(model);
        await _projectService.Create(project);

        var result = await Result<ProjectDto>.SuccessAsync(_mapper.Map<ProjectDto>(project));
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CreateUpdateProject model)
    {
        var project = _mapper.Map<Project>(model);
        project = await _projectService.Update(project);

        if(project.Skills != null)
            foreach (var skill in project.Skills)
                skill.Projects = null;

        var result = await Result<ProjectDto>.SuccessAsync(_mapper.Map<ProjectDto>(project));
        return Ok(result);
    }

    [HttpPut("UpdateSkills")]
    public async Task<IActionResult> UpdateSkills(UpdateProjectSkills model)
    {
        var skills = await _skillService.GetSkillsByIds(model.SkillIds);
        var project = await _projectService.UpdateSkills(model.ProjectId, skills);

        if (project == null)
            BadRequest("Project not found");

        foreach (var skill in project.Skills)
            skill.Projects = null;

        var result = await Result<ProjectDto>.SuccessAsync(_mapper.Map<ProjectDto>(project));
        return Ok(result);
    }

    [HttpPut("UpdateDemoImage/{projectId}")]
    public async Task<IActionResult> SaveSkillImage(int projectId, IFormFile icon)
    {
        var project = await _projectService.GetById(projectId);
        if (project == null)
            throw new Exception("No skill found with the provided id");

        var errorMessage = _uploadImageHelper.ValidateImage(icon);
        if (!string.IsNullOrEmpty(errorMessage))
            throw new Exception(errorMessage);

        project.ImagePath = await _uploadImageHelper.UploadImage(icon);
        project = await _projectService.Update(project);

        var result = await Result<ProjectDto>.SuccessAsync(_mapper.Map<ProjectDto>(project));
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _projectService.Delete(id);

        return Ok(await Result.SuccessAsync("Removed project"));
    }

    #endregion
}
