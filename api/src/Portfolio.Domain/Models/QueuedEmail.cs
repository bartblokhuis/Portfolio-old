using Portfolio.Domain.Models.Common;
using System;

namespace Portfolio.Domain.Models;

public class QueuedEmail : BaseEntity, IHasCreationDate
{
    #region Properties

    public string From { get; set; }

    public string FromName { get; set; }

    public string To { get; set; }

    public string ToName { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public DateTime CreatedAtUTC { get; set; }

    public int SentTries { get; set; }

    public DateTime? SentOnUtc { get; set; }

    #endregion
}
