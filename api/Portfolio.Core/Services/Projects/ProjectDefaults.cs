using Portfolio.Core.Caching;

namespace Portfolio.Core.Services.Projects;

public static class ProjectDefaults
{
    public static string AllProjectsPrefix => "Portfolio.projects.";

    public static CacheKey AllProjectsCacheKey => new CacheKey("Portfolio.projects.all.", AllProjectsPrefix);
}
