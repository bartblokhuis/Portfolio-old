﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Services.Projects;
using Portfolio.Core.Services.Skills;
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
    private readonly IPictureService _pictureService;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public ProjectController(ILogger<ProjectController> logger, IProjectService projectService, IMapper mapper, ISkillService skillService, IPictureService pictureService)
    {
        _logger = logger;
        _projectService = projectService;
        _mapper = mapper;
        _skillService = skillService;
        _pictureService = pictureService;
    }

    #endregion

    #region Methods

    #region Get

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var projects = (await _projectService.GetAllAsync()).ToListResult();

        //Prevent infinit loop issues with the json serializer.
        if(projects.Data != null)
            foreach (var skill in projects.Data.SelectMany(x => x.Skills))
                skill.Projects = null;

        var result = _mapper.Map<ListResult<ProjectDto>>(projects);
        result.Succeeded = true;
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("Published")]
    public async Task<IActionResult> GetPublished()
    {
        var projects = (await _projectService.GetAllPublishedAsync()).ToListResult();

        //Prevent infinit loop issues with the json serializer.
        if (projects.Data != null)
            foreach (var skill in projects.Data.SelectMany(x => x.Skills))
                skill.Projects = null;

        var result = _mapper.Map<ListResult<ProjectDto>>(projects);
        result.Succeeded = true;
        return Ok(result);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int id)
    {
        var project = (await _projectService.GetById(id));

        var result = await Result<ProjectDto>.SuccessAsync(_mapper.Map<ProjectDto>(project));
        result.Succeeded = true;
        return Ok(result);
    }

    [HttpGet("Url/GetByProjectId")]
    public async Task<IActionResult> GetProjectUrlsById(int projectId)
    {
        var urls = (await _projectService.GetProjectUrlsByIdAsync(projectId)).ToListResult();
        var result = _mapper.Map<ListResult<Url>>(urls);
        result.Succeeded = true;
        return Ok(result);
    }

    [HttpGet("Pictures/GetByProjectId")]
    public async Task<IActionResult> GetProjectPicturesById(int projectId)
    {
        var pictures = (await _projectService.GetProjectPicturesByIdAsync(projectId)).ToListResult();
        var result = _mapper.Map<ListResult<ProjectPictureDto>>(pictures);
        result.Succeeded = true;
        return Ok(result);
    }

    #endregion

    #region Post

    [HttpPost]
    public async Task<IActionResult> Create(CreateUpdateProject model)
    {
        var project = _mapper.Map<Project>(model);
        await _projectService.Create(project);

        var result = await Result<ProjectDto>.SuccessAsync(_mapper.Map<ProjectDto>(project));
        return Ok(result);
    }

    [HttpPost("Url/Create")]
    public async Task<IActionResult> Create(CreateProjectUrlDto model)
    {
        if(model == null)
            throw new ArgumentNullException(nameof(model));

        var project = await _projectService.GetById(model.ProjectId);
        if (project == null)
            return Ok(await Result.FailAsync("Project not found"));

        var url = _mapper.Map<Url>(model);
        await _projectService.CreateProjectUrlAsync(project, url);

        var result = await Result.SuccessAsync();
        return Ok(result);
    }

    [HttpPost("Pictures/")]
    public async Task<IActionResult> Create(CreateProjectPictureDto model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        var project = await _projectService.GetById(model.ProjectId);
        if (project == null)
            return Ok(await Result.FailAsync("Project not found"));

        if (project.ProjectPictures != null && project.ProjectPictures.Any(x => x.PictureId == model.PictureId))
            return Ok(await Result.FailAsync("Picture has already been added to the project"));

        var picture = await _pictureService.GetById(model.PictureId);
        if (picture == null)
            return Ok(await Result.FailAsync("Picture not found"));

        await _projectService.CreateProjectPictureAsync(project, picture);
        var result = await Result.SuccessAsync();
        return Ok(result);
    }

    #endregion

    #region Put

    [HttpPut]
    public async Task<IActionResult> Update(CreateUpdateProject model)
    {
        var project = _mapper.Map<Project>(model);
        project = await _projectService.Update(project);

        if (project.Skills != null)
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

    [HttpPut("Pictures/")]
    public async Task<IActionResult> UpdatePicture(UpdateProjectPictureDto model)
    {
        var project = await _projectService.GetById(model.ProjectId);
        if (project == null)
            return Ok(await Result.FailAsync("Project not found"));

        if(project.ProjectPictures == null)
            return Ok(await Result.FailAsync("Current picture not found"));
        
        var projectPicture = project.ProjectPictures.FirstOrDefault(x => x.PictureId == model.CurrentPictureId);
        if (projectPicture == null)
            return Ok(await Result.FailAsync("Current picture not found"));

        if(model.CurrentPictureId != model.NewPictureId)
        {
            if (project.ProjectPictures != null && project.ProjectPictures.Any(x => x.PictureId == model.NewPictureId))
                return Ok(await Result.FailAsync("Picture has already been added to the project"));

            var picture = await _pictureService.GetById(model.NewPictureId);
            if (picture == null)
                return Ok(await Result.FailAsync("Picture not found"));

            projectPicture.PictureId = model.NewPictureId;
        }

        projectPicture.DisplayNumber = model.DisplayNumber;
      
        await _projectService.UpdateProjectPictureAsync(projectPicture);

        return Ok(await Result.SuccessAsync());
    }

    #endregion

    #region Delete

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _projectService.Delete(id);

        return Ok(await Result.SuccessAsync("Removed project"));
    }

    [HttpDelete("Url/Delete")]
    public async Task<IActionResult> DeleteURL(int projectId, int urlId)
    {
        var project = await _projectService.GetById(projectId);
        if(project == null)
            return Ok(await Result.FailAsync("Project not found"));

        await _projectService.DeleteUrl(project, urlId);
        return Ok(await Result.SuccessAsync("Removed project url"));
    }

    [HttpDelete("Pictures/")]
    public async Task<IActionResult> DeletePicture(int projectId, int pictureId)
    {
        var project = await _projectService.GetById(projectId);
        if (project == null)
            return Ok(await Result.FailAsync("Project not found"));

        if(project.ProjectPictures == null)
            return Ok(await Result.FailAsync("Project picture not found"));

        var picture = project.ProjectPictures.FirstOrDefault(x => x.PictureId == pictureId);
        if (picture == null)
            return Ok(await Result.FailAsync("Project picture not found"));

        await _projectService.DeleteProjectPictureAsync(picture);
        return Ok(await Result.SuccessAsync("Removed project picture"));
    }

    #endregion

    #endregion
}
