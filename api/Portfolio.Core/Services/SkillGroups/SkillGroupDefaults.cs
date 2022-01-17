using Portfolio.Core.Caching;

namespace Portfolio.Core.Services.SkillGroups;

public static class SkillGroupDefaults
{
    public static string AllSkillGroupsPrefix => "Portfolio.skill.groups.";

    public static CacheKey AllSkillGroupsCacheKey => new CacheKey("Portfolio.skill.groups.all.", AllSkillGroupsPrefix);
}

