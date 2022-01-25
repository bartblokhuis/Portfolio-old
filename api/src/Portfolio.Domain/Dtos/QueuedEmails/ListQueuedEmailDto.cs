using System;

namespace Portfolio.Domain.Dtos.QueuedEmails;

public class ListQueuedEmailDto
{
    #region Properties

    public int Id { get; set; }

    public string From { get; set; }

    public string FromName { get; set; }

    public string To { get; set; }

    public string ToName { get; set; }

    public string Subject { get; set; }

    public DateTime CreatedAtUTC { get; set; }

    public int SentTries { get; set; }

    public DateTime? SentOnUtc { get; set; }

    #endregion

}
