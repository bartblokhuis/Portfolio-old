using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models.Settings;

public class MessageSettings : BaseEntity, ISetting
{
    #region Properties

    public bool IsSendSiteOwnerEmail { get; set; }

    public string SiteOwnerSubjectTemplate { get; set; }

    public string SiteOwnerTemplate { get; set; }

    public bool IsSendConfirmationEmail { get; set; }

    public string ConfirmationEmailSubjectTemplate { get; set; }

    public string ConfirmationEmailTemplate { get; set; }

    #endregion
}
