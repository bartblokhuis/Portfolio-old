namespace Portfolio.Domain.Dtos.Languages;

public class LanguageBaseDto
{
    #region Properties

    public string Name { get; set; }

    public string LanguageCulture { get; set; }

    public string FlagImageFilePath { get; set; }

    public bool IsPublished { get; set; }

    public int DisplayNumber { get; set; }

    #endregion
}