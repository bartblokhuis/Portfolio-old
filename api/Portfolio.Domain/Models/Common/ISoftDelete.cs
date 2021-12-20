namespace Portfolio.Domain.Models.Common;

public interface ISoftDelete
{
    #region Properties

    public bool IsDeleted { get; set; }

    #endregion
}
