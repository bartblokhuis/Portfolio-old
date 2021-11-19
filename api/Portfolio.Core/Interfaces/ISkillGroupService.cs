using System.Collections.Generic;
using System.Threading.Tasks;
using Portfolio.Domain.Models;

namespace Portfolio.Core.Interfaces
{
    public interface ISkillGroupService
    {
        Task<IEnumerable<SkillGroup>> GetAll();

        Task Insert(SkillGroup skillGroupDto);

        Task Update(SkillGroup skillGroupDto);

        Task Update(IEnumerable<SkillGroup> skillGroupDto);

        Task Delete(SkillGroup skill);

        Task<bool> IsExistingTitle(string title, int idToIgnore = 0);

        Task<bool> Exists(int id);

        Task<SkillGroup> GetById(int id);
    }
}
