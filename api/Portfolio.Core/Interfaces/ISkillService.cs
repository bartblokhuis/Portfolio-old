using Portfolio.Domain.Dtos;
using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces
{
    public interface ISkillService
    {
        Task<IEnumerable<Skill>> GetAll();

        Task<IEnumerable<Skill>> GetBySkillGroupId(int skillGroupId);

        Task Insert(Skill skillDto);

        Task Update(Skill skillDto);

        Task Delete(int id);

        Task<bool> Exists(int id);

        Task<Skill> GetById(int id);

        Task<bool> IsExistingSkill(string name, int skillGroup, Skill skillToIgnor = null);

        Task<IEnumerable<Skill>> GetSkillsByIds(IEnumerable<int> ids);
    }
}
