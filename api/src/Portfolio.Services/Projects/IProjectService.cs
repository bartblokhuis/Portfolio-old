using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Services.Projects;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllAsync();

    Task<IEnumerable<Project>> GetAllPublishedAsync();

    Task<Project> GetByIdAsync(int id);

    Task<IEnumerable<Url>> GetProjectUrlsByIdAsync(int id);

    Task<IEnumerable<ProjectPicture>> GetProjectPicturesByIdAsync(int id);

    Task InsertAsync(Project model);

    Task InsertProjectUrlAsync(Project project, Url url);

    Task InsertProjectPictureAsync(Project project, Picture picture);

    Task<Project> UpdateAsync(Project model);

    Task<Project> UpdateSkillsAsync(int projectId, IEnumerable<Skill> skills);

    Task UpdateProjectPictureAsync(ProjectPicture picture);

    Task DeleteAsync(int id);

    Task DeleteProjectUrlAsync(Project project, int urlId);

    Task DeleteProjectPictureAsync(ProjectPicture projectPicture);

}