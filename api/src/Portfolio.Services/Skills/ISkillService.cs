using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Services.Skills;
public interface ISkillService
{
    Task<IEnumerable<Skill>> GetAllAsync();

    Task<IEnumerable<Skill>> GetBySkillGroupIdAsync(int skillGroupId);

    Task InsertAsync(Skill skillDto);

    Task UpdateAsync(Skill skillDto);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);

    Task<Skill> GetByIdAsync(int id);

    Task<bool> IsExistingSkillAsync(string name, int skillGroup, Skill skillToIgnor = null);

    Task<IEnumerable<Skill>> GetSkillsByIdsAsync(IEnumerable<int> ids);
}
