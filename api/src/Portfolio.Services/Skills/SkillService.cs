using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.Services.Common;
using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Services.Repository;

namespace Portfolio.Services.Skills;
public class SkillService : ISkillService
{
    #region Fields

    private readonly IBaseRepository<Skill> _skillRepository;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public SkillService(IBaseRepository<Skill> skillRepository, IMapper mapper)
    {
        _skillRepository = skillRepository;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    public Task DeleteAsync(int id)
    {
        return _skillRepository.DeleteAsync(id);
    }

    public Task<Skill> GetByIdAsync(int id)
    {
        return _skillRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Skill>> GetAllAsync()
    {
        var skills = await _skillRepository.GetAllAsync(query => query.OrderByDescending(skill => skill.SkillGroupId).ThenBy(skill => skill.DisplayNumber));
        return skills;
    }

    public async Task<IEnumerable<Skill>> GetBySkillGroupIdAsync(int skillGroupId)
    {
        var skills = await _skillRepository.GetAllAsync(query => query.Where(skill => skill.SkillGroupId == skillGroupId).OrderByDescending(skill => skill.SkillGroupId).ThenBy(skill => skill.DisplayNumber));
        return skills;
    }

    public async Task<IEnumerable<Skill>> GetSkillsByIdsAsync(IEnumerable<int> ids)
    {
        var skills = await _skillRepository.GetAllAsync(query => query.Where(skill => ids.Contains(skill.Id)));
        return skills;
    }

    public Task InsertAsync(Skill skillDto)
    {
        return _skillRepository.InsertAsync(skillDto);
    }

    public Task UpdateAsync(Skill skillDto)
    {
        return _skillRepository.UpdateAsync(skillDto);
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _skillRepository.Table.AnyAsync(skill => skill.Id == id);
    }

    public Task<bool> IsExistingSkillAsync(string name, int skillGroup, Skill skillToIgnore = null)
    {
        return _skillRepository.Table.Where(x => x.SkillGroupId == skillGroup).AnyAsync(x => x.Name.ToLower() == name.ToLower() && (skillToIgnore == null || x.Id != skillToIgnore.Id));
    }

    #endregion
}
