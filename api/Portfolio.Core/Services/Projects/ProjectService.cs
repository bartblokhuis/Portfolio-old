using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Core.Services.Urls;
using Portfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Projects;

public class ProjectService : IProjectService
{
    #region Fields

    private readonly IBaseRepository<Project> _projectRepository;
    private readonly IBaseRepository<ProjectUrls> _projectUrlsRepository;
    private readonly IUrlService _urlService;

    #endregion

    #region Constructor

    public ProjectService(IBaseRepository<Project> projectRepository, IUrlService urlService, IBaseRepository<ProjectUrls> projectUrlsRepository)
    {
        _projectRepository = projectRepository;
        _urlService = urlService;
        _projectUrlsRepository = projectUrlsRepository;
    }

    #endregion

    #region Methods

    #region Get

    public async Task<IEnumerable<Project>> Get()
    {
        var projects = await _projectRepository.GetAllAsync(query => query.Include(x => x.Skills).Include(x => x.ProjectUrls).ThenInclude(x => x.Url),
            cache => cache.PrepareKeyForDefaultCache(ProjectDefaults.AllProjectsCacheKey));
        return projects;
    }

    public async Task<Project> GetById(int id)
    {
        var projects = await _projectRepository.GetAllAsync(query => query.Include(x => x.Skills).Include(x => x.ProjectUrls).ThenInclude(x => x.Url).Where(x => x.Id == id));
        return projects == null ? null : projects.First();
    }

    public async Task<IEnumerable<Url>> GetProjectUrlsByIdAsync(int id)
    {
        var project = await GetById(id);

        return project == null ? null : project.ProjectUrls.Select(x => x.Url);

    }

    #endregion

    #region Create

    public async Task Create(Project model)
    {
        await _projectRepository.InsertAsync(model);
    }

    public async Task CreateProjectUrlAsync(Project project, Url url)
    {
        var projectUrl = new ProjectUrls(project, url);
        await _projectUrlsRepository.InsertAsync(projectUrl);
    }

    #endregion

    #region Update

    public async Task<Project> Update(Project model)
    {
        await _projectRepository.UpdateAsync(model);
        return model;
    }

    public async Task<Project> UpdateSkills(int projectId, IEnumerable<Skill> skills)
    {
        var project = _projectRepository.Table.Include(x => x.Skills).FirstOrDefault(x => x.Id == projectId);
        if (project == null)
            return null;

        var skillIds = skills.Select(x => x.Id);

        project.Skills ??= new List<Skill>();

        foreach (var skill in project.Skills.Where(x => !skillIds.Contains(x.Id)))
        {
            project.Skills.Remove(skill);
        }

        foreach (var skill in skills.Where(x => project.Skills.All(y => y.Id != x.Id)))
        {
            project.Skills.Add(skill);
        }

        await _projectRepository.UpdateAsync(project);
        return project;
    }

    #endregion

    #region Delete

    public async Task Delete(int id)
    {
        await _projectRepository.DeleteAsync(id);
        return;
    }

    public async Task DeleteUrl(Project project, int urlId)
    {
        if (project == null)
            throw new ArgumentNullException(nameof(project));

        if (project.ProjectUrls == null)
            return;

        project.ProjectUrls = project.ProjectUrls.Where(x => x.UrlId != urlId).ToList();
        await Update(project);

        await _urlService.DeleteAsync(urlId);
    }

    #endregion

    #endregion
}
