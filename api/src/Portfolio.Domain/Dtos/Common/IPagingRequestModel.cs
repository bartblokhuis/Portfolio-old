namespace Portfolio.Domain.Dtos.Common;

public interface IPagingRequestModel
{
    public int Page { get; }

    public int PageSize { get; }
}
