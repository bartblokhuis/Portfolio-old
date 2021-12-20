using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models;

public class SeoSettings : BaseEntity, ISetting
{
    #region Properties

    public string Title { get; set; }

    public string DefaultMetaKeywords { get; set; }

    public string DefaultMetaDescription { get; set; }

    public bool UseTwitterMetaTags { get; set; }

    public bool UseOpenGraphMetaTags { get; set; }

    #endregion
}
