using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces;

public interface ISkillGroupService
{
    Task<IEnumerable<SkillGroup>> GetAll(bool includeSkills = true);

    Task Insert(SkillGroup skillGroupDto);

    Task Update(SkillGroup skillGroupDto);

    Task Update(IQueryable<SkillGroup> skillGroupDto);

    Task Delete(SkillGroup skill);

    Task<bool> IsExistingTitle(string title, int idToIgnore = 0);

    Task<bool> Exists(int id);

    Task<SkillGroup> GetById(int id);
}
