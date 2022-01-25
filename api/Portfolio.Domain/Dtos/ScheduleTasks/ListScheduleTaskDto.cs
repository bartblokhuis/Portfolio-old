using System;

namespace Portfolio.Domain.Dtos.ScheduleTasks;

public class ListScheduleTaskDto : BaseScheduleTaskDto
{
    #region Properties

    public int Id { get; set; }

    public DateTime? LastStartUtc { get; set; }

    public DateTime? LastEndUtc { get; set; }

    public DateTime? LastSuccessUtc { get; set; }

    #endregion
}
