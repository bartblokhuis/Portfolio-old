using System;

namespace Portfolio.Domain.Dtos.BlogSubscribers;

public class BlogSubscriberDto
{
    #region Properties

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string EmailAddress { get; set; }

    public DateTime CreatedAtUTC { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    #endregion
}
