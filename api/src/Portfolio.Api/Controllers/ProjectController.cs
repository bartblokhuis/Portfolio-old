using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Domain.Dtos.Common;
using Portfolio.Domain.Dtos.Projects;
using Portfolio.Domain.Extensions;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;
using Portfolio.Services.Pictures;
using Portfolio.Services.Projects;
using Portfolio.Services.Skills;
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

    #region Utils

    private async Task<string> Validate(CreateUpdateProjectDto dto, int projectId = 0)
    {
        if (string.IsNullOrEmpty(dto.Title))
            return "Please enter the projects title";

        if (dto.Title.Length > 64)
            return "Please don't enter a title with more than 64 character";

        if (await _projectService.IsExistingTitleAsync(dto.Title, projectId))
            return "There is already a project with the same title";

        if (dto.Description?.Length > 512)
            return "Please don't enter a description with more than 512 character";

        return "";
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
        var project = (await _projectService.GetByIdAsync(id));

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

    [HttpPost("List")]
    public async Task<IActionResult> List(BaseSearchModel baseSearchModel)
    {
        var projects = await _projectService.GetAllProjectsAsync(pageIndex: baseSearchModel.Page - 1, pageSize: baseSearchModel.PageSize);

        var model = await new ProjectListDto().PrepareToGridAsync(baseSearchModel, projects, () =>
        {
            return projects.ToAsyncEnumerable().SelectAwait(async project => _mapper.Map<ProjectDto>(project));
        });

        var result = await Result<ProjectListDto>.SuccessAsync(model);
        return Ok(result);
    }

    [HttpPost("Pictures/List")]
    public async Task<IActionResult> PicturesList(ProjectPictureSearchModel searchModel)
    {
        var projects = await _projectService.GetAllProjectPicturesAsync(searchModel.ProjectId, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

        var model = await new ProjectPictureListDto().PrepareToGridAsync(searchModel, projects, () =>
        {
            return projects.ToAsyncEnumerable().SelectAwait(async project => _mapper.Map<ProjectPictureDto>(project));
        });

        var result = await Result<ProjectPictureListDto>.SuccessAsync(model);
        return Ok(result);
    }

    [HttpPost("Urls/List")]
    public async Task<IActionResult> UrlsList(ProjectUrlSearchDto searchModel)
    {
        var projects = await _projectService.GetAllProjectUrlsAsync(searchModel.ProjectId, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

        var model = await new ProjectUrlListDto().PrepareToGridAsync(searchModel, projects, () =>
        {
            return projects.ToAsyncEnumerable().SelectAwait(async project => project.Url);
        });

        var result = await Result<ProjectUrlListDto>.SuccessAsync(model);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectDto model)
    {
        var error = await Validate(model);

        if (!string.IsNullOrEmpty(error))
            return Ok(await Result.FailAsync(error));

        var project = _mapper.Map<Project>(model);
        await _projectService.InsertAsync(project);

        var result = await Result<ProjectDto>.SuccessAsync(_mapper.Map<ProjectDto>(project));
        return Ok(result);
    }

    [HttpPost("Url/Create")]
    public async Task<IActionResult> Create(CreateProjectUrlDto model!!)
    {
        var project = await _projectService.GetByIdAsync(model.ProjectId);
        if (project == null)
            return Ok(await Result.FailAsync("Project not found"));

        var url = _mapper.Map<Url>(model);
        await _projectService.InsertProjectUrlAsync(project, url);

        var result = await Result.SuccessAsync();
        return Ok(result);
    }

    [HttpPost("Pictures/")]
    public async Task<IActionResult> Create(CreateProjectPictureDto model!!)
    {
        var project = await _projectService.GetByIdAsync(model.ProjectId);
        if (project == null)
            return Ok(await Result.FailAsync("Project not found"));

        if (project.ProjectPictures != null && project.ProjectPictures.Any(x => x.PictureId == model.PictureId))
            return Ok(await Result.FailAsync("Picture has already been added to the project"));

        var picture = await _pictureService.GetByIdAsync(model.PictureId);
        if (picture == null)
            return Ok(await Result.FailAsync("Picture not found"));

        await _projectService.InsertProjectPictureAsync(project, picture);
        var result = await Result.SuccessAsync();
        return Ok(result);
    }

    #endregion

    #region Put

    [HttpPut]
    public async Task<IActionResult> Update(UpdateProjectDto model)
    {
        var error = await Validate(model, model.Id);

        if (!string.IsNullOrEmpty(error))
            return Ok(await Result.FailAsync(error));

        var project = _mapper.Map<Project>(model);
        project = await _projectService.UpdateAsync(project);

        if (project.Skills != null)
            foreach (var skill in project.Skills)
                skill.Projects = null;

        var result = await Result<ProjectDto>.SuccessAsync(_mapper.Map<ProjectDto>(project));
        return Ok(result);
    }

    [HttpPut("UpdateSkills")]
    public async Task<IActionResult> UpdateSkills(UpdateProjectSkills model)
    {
        var skills = await _skillService.GetSkillsByIdsAsync(model.SkillIds);
        var project = await _projectService.UpdateSkillsAsync(model.ProjectId, skills);

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
        var project = await _projectService.GetByIdAsync(model.ProjectId);
        if (project == null)
            return Ok(await Result.FailAsync("Project not found"));

        if(project.ProjectPictures == null)
            return Ok(await Result.FailAsync("Current picture not found"));
        
        var projectPicture = project.ProjectPictures.FirstOrDefault(x => x.PictureId == model.CurrentPictureId);
        if (projectPicture == null)
            return Ok(await Result.FailAsync("Current picture not found"));

        if (model.CurrentPictureId == model.NewPictureId || (project.ProjectPictures != null && project.ProjectPictures.Any(x => x.PictureId == model.NewPictureId)))
            return Ok(await Result.FailAsync("Picture has already been added to the project"));

        var picture = await _pictureService.GetByIdAsync(model.NewPictureId);
        if (picture == null)
            return Ok(await Result.FailAsync("Picture not found"));

        projectPicture.PictureId = model.NewPictureId;
        projectPicture.DisplayNumber = model.DisplayNumber;
        await _projectService.UpdateProjectPictureAsync(projectPicture);

        return Ok(await Result.SuccessAsync());
    }

    #endregion

    #region Delete

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _projectService.DeleteAsync(id);

        return Ok(await Result.SuccessAsync("Removed project"));
    }

    [HttpDelete("Url/Delete")]
    public async Task<IActionResult> DeleteURL(int projectId, int urlId)
    {
        var project = await _projectService.GetByIdAsync(projectId);
        if(project == null)
            return Ok(await Result.FailAsync("Project not found"));

        await _projectService.DeleteProjectUrlAsync(project, urlId);
        return Ok(await Result.SuccessAsync("Removed project url"));
    }

    [HttpDelete("Pictures/")]
    public async Task<IActionResult> DeletePicture(int projectId, int pictureId)
    {
        var project = await _projectService.GetByIdAsync(projectId);
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
