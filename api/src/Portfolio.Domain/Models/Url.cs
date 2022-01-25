using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models;
public class Url : BaseEntity
{
    #region Properties

    public string FullUrl  { get; set; }

    public string FriendlyName { get; set; }

    #endregion
}

