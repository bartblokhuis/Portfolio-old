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
    private readonly CacheService _cacheService;
    private const string CACHE_KEY = "SKILL.GROUPS.";

    #endregion

    #region Constructor

    public SkillGroupService(IBaseRepository<SkillGroup> skillGroupRepository, CacheService cacheService)
    {
        _skillGroupRepository = skillGroupRepository;
        _cacheService = cacheService;
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<SkillGroup>> GetAll(bool includeSkills = true)
    {
        var cacheKey = includeSkills ? CACHE_KEY + "LIST.INCLUDE.SKILLS" : CACHE_KEY + "LIST";

        var skillGroups = _cacheService.Get<IEnumerable<SkillGroup>>(cacheKey);
        if (skillGroups != null)
            return skillGroups;



        var queryableSkillGroups = includeSkills ?
            await _skillGroupRepository.GetAsync(orderBy: (s) => s.OrderBy(x => x.DisplayNumber), includeProperties: "Skills"):
            await _skillGroupRepository.GetAsync(orderBy: (s) => s.OrderBy(x => x.DisplayNumber));

        if (queryableSkillGroups != null)
            _cacheService.Set(cacheKey, await queryableSkillGroups.ToListAsync());

        return queryableSkillGroups;
    }

    public async Task<SkillGroup> GetById(int id)
    {
        var cacheKey = CACHE_KEY + id;
        var skillGroup = _cacheService.Get<SkillGroup>(cacheKey);
        if (skillGroup != null)
            return skillGroup;

        skillGroup = await _skillGroupRepository.GetByIdAsync(id);
        if (skillGroup != null)
            _cacheService.Set(cacheKey, skillGroup);

        return skillGroup;
    }

    public async Task Insert(SkillGroup skillGroupDto)
    {
        await _skillGroupRepository.InsertAsync(skillGroupDto);
        ClearListCache();
        _cacheService.Set(CACHE_KEY + skillGroupDto.Id, skillGroupDto);
    }

    public async Task Update(SkillGroup skillGroupDto)
    {
        await _skillGroupRepository.UpdateAsync(skillGroupDto);
        ClearListCache();
        _cacheService.Set(CACHE_KEY + skillGroupDto.Id, skillGroupDto);

    }

    public async Task Update(IQueryable<SkillGroup> skillGroupsDto)
    {
        await _skillGroupRepository.UpdateRangeAsync(skillGroupsDto);
        ClearListCache();
        foreach(var skillGroup in skillGroupsDto)
            _cacheService.Set(CACHE_KEY + skillGroup.Id, skillGroup);
    }
        
    public async Task Delete(SkillGroup skillGroup)
    {
        await _skillGroupRepository.DeleteAsync(skillGroup);
        ClearListCache();
        _cacheService.Set<SkillGroup>(CACHE_KEY + skillGroup.Id, null);
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

    private void ClearListCache()
    {
        var cacheKey1 = CACHE_KEY + "LIST.INCLUDE.SKILLS";
        var cacheKey2 = CACHE_KEY + "LIST";
        _cacheService.Set<IEnumerable<Project>>(cacheKey1, null);
        _cacheService.Set<IEnumerable<Project>>(cacheKey2, null);
    }

    #endregion

    #endregion
}
