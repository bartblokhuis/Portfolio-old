using Portfolio.Domain.Models;

namespace Portfolio.Services.Tasks;

public interface IScheduleTaskService
{
    Task DeleteTaskAsync(ScheduleTask task);

    Task<ScheduleTask> GetTaskByIdAsync(int taskId);

    Task<ScheduleTask> GetTaskByTypeAsync(string type);

    Task<IList<ScheduleTask>> GetAllTasksAsync(bool showHidden = false);

    Task InsertTaskAsync(ScheduleTask task);

    Task UpdateTaskAsync(ScheduleTask task);
}
