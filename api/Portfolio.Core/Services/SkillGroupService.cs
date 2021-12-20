using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Models;

namespace Portfolio.Core.Services;

public class SkillGroupService : ISkillGroupService
{
    #region Fields

    private readonly IBaseRepository<SkillGroup> _skillGroupRepository;

    #endregion

    #region Constructor

    public SkillGroupService(IBaseRepository<SkillGroup> skillGroupRepository)
    {
        _skillGroupRepository = skillGroupRepository;
    }

    #endregion

    #region Methods

    public async Task<IQueryable<SkillGroup>> GetAll(bool includeSkills = true)
    {
        return (includeSkills) ?
            await _skillGroupRepository.GetAsync(orderBy: (s) => s.OrderBy(x => x.DisplayNumber), includeProperties: "Skills"):
            await _skillGroupRepository.GetAsync(orderBy: (s) => s.OrderBy(x => x.DisplayNumber));
    }

    public Task<SkillGroup> GetById(int id)
    {
        return _skillGroupRepository.GetByIdAsync(id);
    }

    public async Task Insert(SkillGroup skillGroupDto)
    {
        await _skillGroupRepository.InsertAsync(skillGroupDto);
    }

    public Task Update(SkillGroup skillGroupDto)
    {
        return _skillGroupRepository.UpdateAsync(skillGroupDto);

    }

    public Task Update(IQueryable<SkillGroup> skillGroupsDto)
    {
        return _skillGroupRepository.UpdateRangeAsync(skillGroupsDto);
    }
        
    public Task Delete(SkillGroup skillGroup)
    {
        return _skillGroupRepository.DeleteAsync(skillGroup);
    }

    #region Utils

    public Task<bool> IsExistingTitle(string title, int idToIgnore = 0)
    {
        return _skillGroupRepository.Table.AnyAsync(skillGroup => skillGroup.Title.ToLower() == title.ToLower() 
                && (idToIgnore == 0 || skillGroup.Id == idToIgnore));
    }

    public Task<bool> Exists(int id)
    {
        return _skillGroupRepository.Table.AnyAsync(skillGroup => skillGroup.Id == id);
    }

    #endregion

    #endregion
}
