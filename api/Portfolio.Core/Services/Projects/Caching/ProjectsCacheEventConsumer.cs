using Portfolio.Core.Caching;
using Portfolio.Domain.Models;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Projects.Caching;

public class ProjectsCacheEventConsumer : CacheEventConsumer<Project, int>
{
    protected override async Task ClearCacheAsync(Project entity, EntityEventType entityEventType)
    {
        await RemoveByPrefixAsync(ProjectDefaults.AllProjectsPrefix, entity);
        await base.ClearCacheAsync(entity, entityEventType);
    }
}
