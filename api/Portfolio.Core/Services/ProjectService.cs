using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Services;

public class ProjectService : IProjectService
{
    #region Fields

    private readonly IBaseRepository<Project> _projectRepository;
    private readonly CacheService _cacheService;
    private const string CACHE_KEY = "PROJECT.";

    #endregion

    #region Constructor

    public ProjectService(IBaseRepository<Project> projectRepository, CacheService cacheService)
    {
        _projectRepository = projectRepository;
        _cacheService = cacheService;
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<Project>> Get()
    {
        var cacheKey = CACHE_KEY + "LIST";
        var projects = _cacheService.Get<IEnumerable<Project>>(cacheKey);
        if (projects != null)
            return projects;

        var queryableProjects = await _projectRepository.GetAsync(includeProperties: "Skills");
        if (queryableProjects != null)
            _cacheService.Set(cacheKey, await queryableProjects.ToListAsync());

        return queryableProjects;
    }

    public async Task<Project> GetById(int id)
    {
        var cacheKey = CACHE_KEY + id;
        var project = _cacheService.Get<Project>(cacheKey);
        if (project != null)
            return project;

        project = await _projectRepository.GetByIdAsync(id);
        if (project != null)
            _cacheService.Set(cacheKey, project);

        return project;
    }

    public async Task Create(Project model)
    {
        await _projectRepository.InsertAsync(model);
        _cacheService.Set(CACHE_KEY + model.Id, model);
        ClearListCache();
    }

    public async Task<Project> Update(Project model)
    {
        await _projectRepository.UpdateAsync(model);
        _cacheService.Set(CACHE_KEY + model.Id, model);
        ClearListCache();
        return model;
    }

    public async Task<Project> UpdateSkills(int projectId, IEnumerable<Skill> skills)
    {
        var project = _projectRepository.Table.Include(x => x.Skills).FirstOrDefault(x => x.Id == projectId);
        if (project == null)
            return null;


        var skillIds = skills.Select(x => x.Id);

        project.Skills ??= new List<Skill>();

        foreach(var skill in project.Skills.Where(x => !skillIds.Contains(x.Id)))
        {
            project.Skills.Remove(skill);
        }

        foreach (var skill in skills.Where(x => project.Skills.All(y => y.Id != x.Id)))
        {
            project.Skills.Add(skill);
        }

        await _projectRepository.UpdateAsync(project);
        _cacheService.Set(CACHE_KEY + project.Id, project);
        ClearListCache();
        return project;
    }

    public async Task Delete(int id)
    {
        await _projectRepository.DeleteAsync(id);
        _cacheService.Set<Project>(CACHE_KEY + id, null);
        ClearListCache();
        return;
    }

    #endregion

    #region Utils

    private void ClearListCache()
    {
        var cacheKey = CACHE_KEY + "LIST";
        _cacheService.Set<IEnumerable<Project>>(cacheKey, null);
    }

    #endregion
}
