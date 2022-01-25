using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.SkillGroups;

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
