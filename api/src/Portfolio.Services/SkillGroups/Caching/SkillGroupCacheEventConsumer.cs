﻿using Portfolio.Core.Caching;
using Portfolio.Domain.Models;

namespace Portfolio.Services.SkillGroups.Caching;

public class SkillGroupCacheEventConsumer : CacheEventConsumer<SkillGroup, int>
{
    protected override async Task ClearCacheAsync(SkillGroup entity, EntityEventType entityEventType)
    {
        await RemoveByPrefixAsync(SkillGroupDefaults.AllSkillGroupsPrefix, entity);
        await base.ClearCacheAsync(entity, entityEventType);
    }
}
