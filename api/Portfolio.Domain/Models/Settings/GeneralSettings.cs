using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models.Settings;

public class GeneralSettings : BaseEntity, ISetting
{
    #region Properties

    public string LandingTitle { get; set; }

    public string LandingDescription { get; set; }

    public string CallToActionText { get; set; }

    public string LinkedInUrl { get; set; }

    public string GithubUrl { get; set; }

    public string StackOverFlowUrl { get; set; }

    public string FooterText { get; set; }

    public bool ShowCopyRightInFooter { get; set; }

    public bool FooterTextBetweenCopyRightAndYear { get; set; }

    public bool ShowContactMeForm { get; set; }

    #endregion
}
