using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models.Settings;

public class PublicSiteSettings : BaseEntity, ISetting
{
    #region Properties

    public string PublicSiteUrl { get; set; }

    #endregion
}
