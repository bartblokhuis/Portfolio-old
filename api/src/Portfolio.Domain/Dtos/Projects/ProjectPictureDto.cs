namespace Portfolio.Domain.Dtos.Projects;

public class ProjectPictureDto
{
    #region Properties

    public int DisplayNumber { get; set; }

    public int PictureId { get; set; }

    public string MimeType { get; set; }

    public string Path { get; set; }

    public string AltAttribute { get; set; }

    public string TitleAttribute { get; set; }

    #endregion
}
