using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models.Localization;

public class Language : BaseEntity, IHasDisplayNumber
{
    #region Properties

    public string Name { get; set; }

    public string LanguageCulture { get; set; }

    public string FlagImageFilePath { get; set; }

    public bool IsPublished { get; set; }

    public int DisplayNumber { get; set; }

    #endregion
}

