using Portfolio.Domain.Dtos.Common;

namespace Portfolio.Domain.Dtos.Languages;

public record LanguageSearchDto : BaseSearchModel
{
    #region Properties

    public bool OnlyShowPublished { get; set; }

    #endregion
}
