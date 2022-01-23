using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Skills;
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

    public Task Delete(int id)
    {
        return _skillRepository.DeleteAsync(id);
    }

    public Task<Skill> GetById(int id)
    {
        return _skillRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Skill>> GetAll()
    {
        var skills = await _skillRepository.GetAllAsync(query => query.OrderByDescending(skill => skill.SkillGroupId).ThenBy(skill => skill.DisplayNumber));
        return skills;
    }

    public async Task<IEnumerable<Skill>> GetBySkillGroupId(int skillGroupId)
    {
        var skills = await _skillRepository.GetAllAsync(query => query.Where(skill => skill.SkillGroupId == skillGroupId).OrderByDescending(skill => skill.SkillGroupId).ThenBy(skill => skill.DisplayNumber));
        return skills;
    }

    public async Task<IEnumerable<Skill>> GetSkillsByIds(IEnumerable<int> ids)
    {
        var skills = await _skillRepository.GetAllAsync(query => query.Where(skill => ids.Contains(skill.Id)));
        return skills;
    }

    public Task Insert(Skill skillDto)
    {
        return _skillRepository.InsertAsync(skillDto);
    }

    public Task Update(Skill skillDto)
    {
        return _skillRepository.UpdateAsync(skillDto);
    }

    #region Utils

    public Task<bool> Exists(int id)
    {
        return _skillRepository.Table.AnyAsync(skill => skill.Id == id);
    }

    public Task<bool> IsExistingSkill(string name, int skillGroup, Skill skillToIgnore = null)
    {
        return _skillRepository.Table.Where(x => x.SkillGroupId == skillGroup).AnyAsync(x => x.Name.ToLower() == name.ToLower() && (skillToIgnore == null || x.Id != skillToIgnore.Id));
    }

    #endregion

    #endregion
}
