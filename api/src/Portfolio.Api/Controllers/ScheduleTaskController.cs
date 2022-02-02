using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Configuration;
using Portfolio.Domain.Dtos.ScheduleTasks;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;
using Portfolio.Services.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ScheduleTaskController : ControllerBase
{
    #region Fields

    private readonly IScheduleTaskService _scheduleTaskService;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;

    #endregion

    #region Constructors

    public ScheduleTaskController(IScheduleTaskService scheduleTaskService, IMapper mapper, AppSettings appSettings)
    {
        _scheduleTaskService = scheduleTaskService;
        _mapper = mapper;
        _appSettings = appSettings;
    }

    #endregion

    #region Utils

    private string Validate(BaseScheduleTaskDto dto)
    {
        if (dto == null)
            return "Unkown error";

        if (string.IsNullOrEmpty(dto.Type))
            return "Please enter the task type";

        if (string.IsNullOrEmpty(dto.Name))
            return "Please enter the schedule task name";

        if (dto.Name.Length > 128)
            return "Please don't enter a name with more than 128 character";

        if (dto.Seconds < 1)
            return "The run time can't be less than one second";

        return "";
    }

    #endregion

    #region Methods

    #region Get

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var scheduledTasks = (await _scheduleTaskService.GetAllTasksAsync(true)).ToListResult();

        var result = _mapper.Map<ListResult<ListScheduleTaskDto>>(scheduledTasks);
        result.Succeeded = true;
        return Ok(result);
    }

    #endregion

    #region Post

    [HttpPost]
    public async Task<IActionResult> Create(CreateScheduleTaskDto dto)
    {
        if (_appSettings.IsDemo)
            return Ok(await Result.FailAsync("Creating a schedule task is not allowed in the demo application"));

        var error = Validate(dto);
        if (!string.IsNullOrEmpty(error))
            return Ok(await Result.FailAsync(error));

        var scheduleTask = _mapper.Map<ScheduleTask>(dto);

        //Save the changes
        await _scheduleTaskService.InsertTaskAsync(scheduleTask);

        var result = await Result<ListScheduleTaskDto>.SuccessAsync(_mapper.Map<ListScheduleTaskDto>(scheduleTask));
        return Ok(result);
    }

    [HttpPost("runtask")]
    [AllowAnonymous]
    public async Task<IActionResult> RunTask(RunScheduleTaskDto scheduleTaskDto)
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

    #region Put

    [HttpPut]
    public async Task<IActionResult> Edit(UpdateScheduleTaskDto dto)
    {
        if (_appSettings.IsDemo)
            return Ok(await Result.FailAsync("Updating a schedule task is not allowed in the demo application"));

        var error = Validate(dto);
        if(!string.IsNullOrEmpty(error))
            return Ok(await Result.FailAsync(error));

        //Get the original task
        var originalTask = await _scheduleTaskService.GetTaskByIdAsync(dto.Id);
        if (originalTask == null)
            return Ok(await Result.FailAsync("Task not found"));

        //Update the updated fields
        _mapper.Map(dto, originalTask);

        //Save the changes
        await _scheduleTaskService.UpdateTaskAsync(originalTask);

        var result = await Result<ListScheduleTaskDto>.SuccessAsync(_mapper.Map<ListScheduleTaskDto>(originalTask));
        return Ok(result);
    }

    #endregion

    #region Delete

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        if (_appSettings.IsDemo)
            return Ok(await Result.FailAsync("Deleting a schedule task is not allowed in the demo application"));

        var scheduleTask = await _scheduleTaskService.GetTaskByIdAsync(id);
        if (scheduleTask == null)
            return Ok(await Result.FailAsync("Task not found"));

        await _scheduleTaskService.DeleteTaskAsync(scheduleTask);

        var result = await Result.SuccessAsync("removed the task");
        return Ok(result);
    }

    #endregion

    #endregion
}
