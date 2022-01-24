using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Services.Tasks;
using Portfolio.Domain.Dtos.ScheduleTasks;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleTaskController : ControllerBase
{
    #region Fields

    private readonly IScheduleTaskService _scheduleTaskService;

    #endregion

    #region Constructors

    public ScheduleTaskController(IScheduleTaskService scheduleTaskService)
    {
        _scheduleTaskService = scheduleTaskService;
    }

    #endregion

    #region Methods

    [HttpPost]
    public async Task<IActionResult> RunTask(ScheduleTaskDto scheduleTaskDto)
    {
        if (scheduleTaskDto == null)
            return NoContent();

        var scheduleTask = await _scheduleTaskService.GetTaskByTypeAsync(scheduleTaskDto.TaskType);
        if (scheduleTask == null)
            //schedule task cannot be loaded
            return NoContent();

        var task = new PortfolioTask(scheduleTask);
        await task.ExecuteAsync();

        return NoContent();
    }

    #endregion
}
