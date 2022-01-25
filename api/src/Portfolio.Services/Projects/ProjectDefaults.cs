using Portfolio.Core.Caching;

namespace Portfolio.Services.Projects;

public static class ProjectDefaults
{
    public static string AllProjectsPrefix => "Portfolio.projects.";

    public static CacheKey AllProjectsCacheKey => new CacheKey("Portfolio.projects.all.", AllProjectsPrefix);

    public static string AllPublishedProjectsPrefix => "Portfolio.projects.published";

    public static CacheKey AllPublishedProjectsCacheKey => new CacheKey("Portfolio.projects.all.published", AllPublishedProjectsPrefix);
}
