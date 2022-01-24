using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Tasks;

public class ScheduleTaskService : IScheduleTaskService
{
    #region Fields

    private readonly IBaseRepository<ScheduleTask> _taskRepository;

    #endregion

    #region Ctor

    public ScheduleTaskService(IBaseRepository<ScheduleTask> taskRepository)
    {
        _taskRepository = taskRepository;
    }

    #endregion

    #region Methods

    public virtual async Task DeleteTaskAsync(ScheduleTask task)
    {
        await _taskRepository.DeleteAsync(task);
    }

    public virtual async Task<ScheduleTask> GetTaskByIdAsync(int taskId)
    {
        return await _taskRepository.GetByIdAsync(taskId);
    }

    public virtual async Task<ScheduleTask> GetTaskByTypeAsync(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
            return null;

        var query = await _taskRepository.GetAllAsync(query =>
        {
            query = query.Where(st => st.Type == type);
            query = query.OrderByDescending(t => t.Id);
            return query;
        });

        return query == null ? null : query.FirstOrDefault();
    }

    public virtual async Task<IList<ScheduleTask>> GetAllTasksAsync(bool showHidden = false)
    {
        var tasks = await _taskRepository.GetAllAsync(query =>
        {
            if (!showHidden)
                query = query.Where(t => t.Enabled);

            query = query.OrderByDescending(t => t.Seconds);

            return query;
        });

        return tasks;
    }

    public virtual async Task InsertTaskAsync(ScheduleTask task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        await _taskRepository.InsertAsync(task);
    }

    public virtual async Task UpdateTaskAsync(ScheduleTask task)
    {
        await _taskRepository.UpdateAsync(task);
    }

    #endregion
}
