using Portfolio.Core.Caching;

namespace Portfolio.Services.SkillGroups;

public static class SkillGroupDefaults
{
    public static string AllSkillGroupsPrefix => "Portfolio.skill.groups.";

    public static CacheKey AllSkillGroupsCacheKey => new CacheKey("Portfolio.skill.groups.all.", AllSkillGroupsPrefix);
}

