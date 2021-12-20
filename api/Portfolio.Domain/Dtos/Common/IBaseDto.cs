namespace Portfolio.Domain.Dtos.Common;

public interface IBaseDto<TKEy>
{
    #region Properties

    public TKEy Id { get; set; }

    #endregion
}

public interface IBaseDto : IBaseDto<int>
{
}
