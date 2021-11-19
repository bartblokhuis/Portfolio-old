using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces
{
    public interface IProjectService
    {
        Task<List<Project>> Get();

        Task<Project> GetById(int id);

        Task Create(Project model);

        Task<Project> Update(Project model);

        Task<Project> UpdateSkills(int projectId, IEnumerable<Skill> skills);

        Task Delete(int id);

    }
}
