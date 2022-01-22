using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models.Settings;

public class BlogSettings : BaseEntity, ISetting
{
    #region Properties

    public bool IsSendEmailOnSubscribing { get; set; }

    public string EmailOnSubscribingTemplate { get; set; }

    public bool IsSendEmailOnPublishing { get; set; }

    public string EmailOnPublishingTemplate { get; set; }

    #endregion
}
