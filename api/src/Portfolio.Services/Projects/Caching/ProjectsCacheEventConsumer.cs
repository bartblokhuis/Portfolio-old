using Portfolio.Core.Caching;
using Portfolio.Domain.Models;
using System.Threading.Tasks;

namespace Portfolio.Services.Projects.Caching;

public class ProjectsCacheEventConsumer : CacheEventConsumer<Project, int>
{
    protected override async Task ClearCacheAsync(Project entity, EntityEventType entityEventType)
    {
        await RemoveAsync(ProjectDefaults.AllProjectsCacheKey);
        await RemoveAsync(ProjectDefaults.AllPublishedProjectsCacheKey);

        await base.ClearCacheAsync(entity, entityEventType);
    }
}
