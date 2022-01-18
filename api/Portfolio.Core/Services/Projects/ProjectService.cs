using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Projects;

public class ProjectService : IProjectService
{
    #region Fields

    private readonly IBaseRepository<Project> _projectRepository;

    #endregion

    #region Constructor

    public ProjectService(IBaseRepository<Project> projectRepository)
    {
        _projectRepository = projectRepository;
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<Project>> Get()
    {
        var projects = await _projectRepository.GetAllAsync(query => query.Include(x => x.Skills),
            cache => cache.PrepareKeyForDefaultCache(ProjectDefaults.AllProjectsCacheKey));
        return projects;
    }

    public async Task<Project> GetById(int id)
    {

        var project = await _projectRepository.GetByIdAsync(id);
        return project;
    }

    public async Task Create(Project model)
    {
        await _projectRepository.InsertAsync(model);
    }

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

        foreach(var skill in project.Skills.Where(x => !skillIds.Contains(x.Id)))
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

    public async Task Delete(int id)
    {
        await _projectRepository.DeleteAsync(id);
        return;
    }

    #endregion
}