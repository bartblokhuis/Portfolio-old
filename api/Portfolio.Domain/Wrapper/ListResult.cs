using System.Collections.Generic;

namespace Portfolio.Domain.Wrapper;

public class ListResult<T> : Result
{
    public ListResult(List<T> data)
    {
        Data = data;
    }

    public List<T> Data { get; set; }

    internal ListResult(bool succeeded, List<T> data = default, List<string> messages = null)
    {
        Data = data;
        Succeeded = succeeded;
    }

    public static ListResult<T> Failure(List<string> messages)
    {
        return new(false, default, messages);
    }

    public static ListResult<T> Success(List<T> data)
    {
        return new(true, data, null);
    }

    public static ListResult<T> Success(List<T> data, string message)
    {
        return new(true, data, new List<string> { message });
    }
}