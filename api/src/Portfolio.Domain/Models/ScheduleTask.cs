using Portfolio.Domain.Models.Common;
using System;

namespace Portfolio.Domain.Models;

public class ScheduleTask : BaseEntity
{
    #region Properties

    public string Name { get; set; }

    public int Seconds { get; set; }

    public string Type { get; set; }

    public bool Enabled { get; set; }

    public bool StopOnError { get; set; }

    public DateTime? LastStartUtc { get; set; }

    public DateTime? LastEndUtc { get; set; }

    public DateTime? LastSuccessUtc { get; set; }

    #endregion
}
