using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models;

public class Picture : BaseEntity
{
    public string MimeType { get; set; }

    public string Path { get; set; }

    public string AltAttribute { get; set; }

    public string TitleAttribute { get; set; }
}

