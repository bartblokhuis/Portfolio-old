using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces;
public interface ISkillService
{
    Task<IQueryable<Skill>> GetAll();

    Task<IQueryable<Skill>> GetBySkillGroupId(int skillGroupId);

    Task Insert(Skill skillDto);

    Task Update(Skill skillDto);

    Task Delete(int id);

    Task<bool> Exists(int id);

    Task<Skill> GetById(int id);

    Task<bool> IsExistingSkill(string name, int skillGroup, Skill skillToIgnor = null);

    Task<IQueryable<Skill>> GetSkillsByIds(IEnumerable<int> ids);
}
