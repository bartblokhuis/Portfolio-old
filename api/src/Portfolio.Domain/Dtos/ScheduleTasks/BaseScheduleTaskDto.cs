namespace Portfolio.Domain.Dtos.ScheduleTasks;

public abstract class BaseScheduleTaskDto
{
    #region Properties

    public string Name { get; set; }

    public int Seconds { get; set; }

    public string Type { get; set; }

    public bool Enabled { get; set; }

    public bool StopOnError { get; set; }

    #endregion
}
