using Portfolio.Domain.Models;

namespace Portfolio.Services.SkillGroups;

public interface ISkillGroupService
{
    Task<IEnumerable<SkillGroup>> GetAllAsync();

    Task InsertAsync(SkillGroup skillGroupDto);

    Task UpdateAsync(SkillGroup skillGroupDto);

    Task UpdateAsync(IQueryable<SkillGroup> skillGroupDto);

    Task DeleteAsync(SkillGroup skill);

    Task<bool> IsExistingTitleAsync(string title, int idToIgnore = 0);

    Task<bool> ExistsAsync(int id);

    Task<SkillGroup> GetByIdAsync(int id);
}
