using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Common;

namespace Portfolio.Services.Projects;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllAsync();

    Task<IEnumerable<Project>> GetAllPublishedAsync();

    Task<IPagedList<Project>> GetAllProjectsAsync(int pageIndex = 0, int pageSize = int.MaxValue);

    Task<IPagedList<ProjectPicture>> GetAllProjectPicturesAsync(int projectId, int pageIndex = 0, int pageSize = int.MaxValue);

    Task<IPagedList<ProjectUrls>> GetAllProjectUrlsAsync(int projectId, int pageIndex = 0, int pageSize = int.MaxValue);

    Task<Project> GetByIdAsync(int id);

    Task<IEnumerable<Url>> GetProjectUrlsByIdAsync(int id);

    Task<IEnumerable<ProjectPicture>> GetProjectPicturesByIdAsync(int id);

    Task<bool> IsExistingTitleAsync(string title, int idToIgnore = 0);

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
