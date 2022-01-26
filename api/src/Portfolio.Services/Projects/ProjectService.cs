using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Models;
using Portfolio.Services.Repository;
using Portfolio.Services.Urls;

namespace Portfolio.Services.Projects;

public class ProjectService : IProjectService
{
    #region Fields

    private readonly IBaseRepository<Project> _projectRepository;
    private readonly IBaseRepository<ProjectUrls> _projectUrlsRepository;
    private readonly IBaseRepository<ProjectPicture> _projectPictureRepository;
    private readonly IUrlService _urlService;

    #endregion

    #region Constructor

    public ProjectService(IBaseRepository<Project> projectRepository, IUrlService urlService, IBaseRepository<ProjectUrls> projectUrlsRepository, IBaseRepository<ProjectPicture> projectPictureRepository)
    {
        _projectRepository = projectRepository;
        _urlService = urlService;
        _projectUrlsRepository = projectUrlsRepository;
        _projectPictureRepository = projectPictureRepository;
    }

    #endregion

    #region Methods

    #region Get

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        var projects = await _projectRepository.GetAllAsync(query => query.Include(x => x.Skills).Include(x => x.ProjectUrls).ThenInclude(x => x.Url).Include(x => x.ProjectPictures.OrderBy(x => x.DisplayNumber)).ThenInclude(x => x.Picture),
            cache => cache.PrepareKeyForDefaultCache(ProjectDefaults.AllProjectsCacheKey));
        return projects;
    }

    public async Task<IEnumerable<Project>> GetAllPublishedAsync()
    {
        var projects = await _projectRepository.GetAllAsync(
            query => query.Include(x => x.Skills).Include(x => x.ProjectUrls).ThenInclude(x => x.Url).Include(x => x.ProjectPictures.OrderBy(x => x.DisplayNumber)).ThenInclude(x => x.Picture).Where(x => x.IsPublished),
            cache => cache.PrepareKeyForDefaultCache(ProjectDefaults.AllPublishedProjectsCacheKey));
        return projects;
    }

    public async Task<Project> GetByIdAsync(int id)
    {
        var projects = await _projectRepository.GetAllAsync(query => query.Include(x => x.Skills).Include(x => x.ProjectUrls).ThenInclude(x => x.Url).Include(x => x.ProjectPictures.OrderBy(x => x.DisplayNumber)).ThenInclude(x => x.Picture).Where(x => x.Id == id));
        return projects == null ? null : projects.First();
    }

    public async Task<IEnumerable<Url>> GetProjectUrlsByIdAsync(int id)
    {
        var project = await GetByIdAsync(id);

        return project == null ? null : project.ProjectUrls.Select(x => x.Url);
    }

    public async Task<IEnumerable<ProjectPicture>> GetProjectPicturesByIdAsync(int id)
    {
        var project = await GetByIdAsync(id);
        return project == null ? null : project.ProjectPictures;
    }

    public Task<bool> IsExistingTitleAsync(string title, int idToIgnore = 0)
    {
        return _projectRepository.Table.AnyAsync(project => project.Title.ToLower() == title.ToLower()
                && (idToIgnore == 0 || project.Id != idToIgnore));
    }

    #endregion

    #region Create

    public async Task InsertAsync(Project model)
    {
        await _projectRepository.InsertAsync(model);
    }

    public async Task InsertProjectUrlAsync(Project project, Url url)
    {
        await _urlService.InsertAsync(url);

        var projectUrl = new ProjectUrls(project.Id, url.Id);
        await _projectUrlsRepository.InsertAsync(projectUrl);
    }

    public async Task InsertProjectPictureAsync(Project project, Picture picture)
    {
        var projectPicture = new ProjectPicture(project.Id, picture.Id);
        await _projectPictureRepository.InsertAsync(projectPicture);
    }

    #endregion

    #region Update

    public async Task<Project> UpdateAsync(Project model)
    {
        await _projectRepository.UpdateAsync(model);
        return model;
    }

    public async Task<Project> UpdateSkillsAsync(int projectId, IEnumerable<Skill> skills)
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

    public Task UpdateProjectPictureAsync(ProjectPicture picture)
    {
        if(picture == null)
            throw new ArgumentNullException(nameof(picture));

        picture.Project = null;
        picture.Picture = null;

        return _projectPictureRepository.UpdateAsync(picture);
    }

    #endregion

    #region Delete

    public async Task DeleteAsync(int id)
    {
        await _projectRepository.DeleteAsync(id);
        return;
    }

    public async Task DeleteProjectUrlAsync(Project project, int urlId)
    {
        if (project == null)
            throw new ArgumentNullException(nameof(project));

        var projectUrl = await _projectUrlsRepository.FirstOrDefaultAsync(x => x.ProjectId == project.Id && x.UrlId == urlId);
        if (projectUrl == null)
            return;

        await _projectUrlsRepository.DeleteAsync(projectUrl);

        try
        {
            await _urlService.DeleteAsync(urlId);
        }
        catch (Exception ex)
        {

        }
    }

    public Task DeleteProjectPictureAsync(ProjectPicture projectPicture)
    {
        return _projectPictureRepository.DeleteAsync(projectPicture);
    }

    #endregion

    #endregion
}
