namespace Portfolio.Domain.Dtos.Settings;

public class MessageSettingsDto
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
