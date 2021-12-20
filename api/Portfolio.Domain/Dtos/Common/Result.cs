namespace Portfolio.Domain.Dtos.Common;

public class Result
{
    public bool Success { get; set; }
    public string Message { get; set; }

    public Result FromSuccess(string message = "")
    {
        return new Result { Success = true, Message = message };
    }

    public Result FromFail(string message)
    {
        return new Result { Success = false, Message = message };
    }
}

public class Result<T> where T : class
{
    public bool Success { get; set; }
    public string Message { get; set; }

    public T Data { get; set; }
}
