using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Projects;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllAsync();

    Task<IEnumerable<Project>> GetAllPublishedAsync();

    Task<Project> GetById(int id);

    Task<IEnumerable<Url>> GetProjectUrlsByIdAsync(int id);

    Task<IEnumerable<ProjectPicture>> GetProjectPicturesByIdAsync(int id);

    Task Create(Project model);

    Task CreateProjectUrlAsync(Project project, Url url);

    Task CreateProjectPictureAsync(Project project, Picture picture);

    Task<Project> Update(Project model);

    Task<Project> UpdateSkills(int projectId, IEnumerable<Skill> skills);

    Task UpdateProjectPictureAsync(ProjectPicture picture);

    Task Delete(int id);

    Task DeleteUrl(Project project, int urlId);

    Task DeleteProjectPictureAsync(ProjectPicture projectPicture);

}
