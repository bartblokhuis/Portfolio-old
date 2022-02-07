using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Common;

namespace Portfolio.Services.Tasks;

public interface IScheduleTaskService
{
    Task DeleteTaskAsync(ScheduleTask task);

    Task<ScheduleTask> GetTaskByIdAsync(int taskId);

    Task<ScheduleTask> GetTaskByTypeAsync(string type);

    Task<IList<ScheduleTask>> GetAllTasksAsync(bool showHidden = false);

    Task<IPagedList<ScheduleTask>> GetAllScheduleTasksAsync(int pageIndex = 0, int pageSize = int.MaxValue);

    Task InsertTaskAsync(ScheduleTask task);

    Task UpdateTaskAsync(ScheduleTask task);
}
