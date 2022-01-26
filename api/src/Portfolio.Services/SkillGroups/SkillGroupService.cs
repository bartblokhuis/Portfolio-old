using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Models;
using Portfolio.Services.Repository;

namespace Portfolio.Services.SkillGroups;

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

    public async Task<IEnumerable<SkillGroup>> GetAllAsync()
    {
        var skillGroups = await _skillGroupRepository.GetAllAsync(query => query.OrderByDescending(x => x.DisplayNumber).Include(x => x.Skills),
            cache => cache.PrepareKeyForDefaultCache(SkillGroupDefaults.AllSkillGroupsCacheKey));

        return skillGroups;
    }

    public async Task<SkillGroup> GetByIdAsync(int id)
    {
        var skillGroup = await _skillGroupRepository.GetByIdAsync(id);
        return skillGroup;
    }

    public async Task InsertAsync(SkillGroup skillGroupDto)
    {
        await _skillGroupRepository.InsertAsync(skillGroupDto);
    }

    public async Task UpdateAsync(SkillGroup skillGroupDto)
    {
        await _skillGroupRepository.UpdateAsync(skillGroupDto);
    }

    public async Task UpdateAsync(IQueryable<SkillGroup> skillGroupsDto)
    {
        await _skillGroupRepository.UpdateRangeAsync(skillGroupsDto);
    }
        
    public async Task DeleteAsync(SkillGroup skillGroup)
    {
        await _skillGroupRepository.DeleteAsync(skillGroup);
    }

    public Task<bool> IsExistingTitleAsync(string title, int idToIgnore = 0)
    {
        return _skillGroupRepository.Table.AnyAsync(skillGroup => skillGroup.Title.ToLower() == title.ToLower()
                && (idToIgnore == 0 || skillGroup.Id != idToIgnore));
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _skillGroupRepository.Table.AnyAsync(skillGroup => skillGroup.Id == id);
    }

    #endregion
}
