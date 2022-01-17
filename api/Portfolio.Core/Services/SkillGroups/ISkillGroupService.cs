using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.SkillGroups;

public interface ISkillGroupService
{
    Task<IEnumerable<SkillGroup>> GetAll();

    Task Insert(SkillGroup skillGroupDto);

    Task Update(SkillGroup skillGroupDto);

    Task Update(IQueryable<SkillGroup> skillGroupDto);

    Task Delete(SkillGroup skill);

    Task<bool> IsExistingTitle(string title, int idToIgnore = 0);

    Task<bool> Exists(int id);

    Task<SkillGroup> GetById(int id);
}
