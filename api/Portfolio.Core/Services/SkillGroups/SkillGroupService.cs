using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.SkillGroups;

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

    public async Task<IEnumerable<SkillGroup>> GetAll()
    {
        var skillGroups = await _skillGroupRepository.GetAllAsync(query => query.OrderByDescending(x => x.DisplayNumber).Include(x => x.Skills),
            cache => cache.PrepareKeyForDefaultCache(SkillGroupDefaults.AllSkillGroupsCacheKey));

        return skillGroups;
    }

    public async Task<SkillGroup> GetById(int id)
    {
        var skillGroup = await _skillGroupRepository.GetByIdAsync(id);
        return skillGroup;
    }

    public async Task Insert(SkillGroup skillGroupDto)
    {
        await _skillGroupRepository.InsertAsync(skillGroupDto);
    }

    public async Task Update(SkillGroup skillGroupDto)
    {
        await _skillGroupRepository.UpdateAsync(skillGroupDto);
    }

    public async Task Update(IQueryable<SkillGroup> skillGroupsDto)
    {
        await _skillGroupRepository.UpdateRangeAsync(skillGroupsDto);
    }
        
    public async Task Delete(SkillGroup skillGroup)
    {
        await _skillGroupRepository.DeleteAsync(skillGroup);
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
