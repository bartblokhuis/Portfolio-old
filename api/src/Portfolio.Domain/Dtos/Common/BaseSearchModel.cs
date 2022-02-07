namespace Portfolio.Domain.Dtos.Common;

public record BaseSearchModel : IPagingRequestModel
{
    #region Constructor

    public BaseSearchModel()
    {
        //Default value
        Length = 10;
    }

    #endregion

    #region Properties

    public int Page => (Start / Length) + 1;

    public int PageSize { get; set; }

    public string AvailablePageSizes { get; set; }

    public string Draw { get; set; }

    public int Start { get; set; }

    public int Length { get; set; }

    #endregion
}
