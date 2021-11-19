using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Services
{
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

        public Task<List<Project>> Get()
        {
            return _projectRepository.Table.AsQueryable().AsNoTracking().Include(x => x.Skills).ToListAsync();
        }

        public Task<Project> GetById(int id)
        {
            return _projectRepository.GetByIdAsync(id);
        }

        public Task Create(Project model)
        {
            return _projectRepository.InsertAsync(model);
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

        public Task Delete(int id)
        {
            return _projectRepository.DeleteAsync(id);
        }

        #region Utils

        #endregion

        #endregion
    }
}
