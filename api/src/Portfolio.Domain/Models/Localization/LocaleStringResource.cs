using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models.Localization;

public class LocaleStringResource : BaseEntity
{
    #region Properties

    public string Area { get; set; }

    public string Page { get; set; }

    public string ResourceName { get; set; }

    public string ResourceValue { get; set; }

    public Language Language { get; set; }

    public int LanguageId { get; set; }

    #endregion
}

