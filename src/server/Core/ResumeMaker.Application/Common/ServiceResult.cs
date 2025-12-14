namespace ResumeMaker.Application.Common;

public class ServiceResult
{
    public bool IsSuccess { get; }
    public string? Message { get; }
    public IReadOnlyList<string> Errors { get; }

    protected ServiceResult(bool isSuccess, string? message, IReadOnlyList<string>? errors)
    {
        IsSuccess = isSuccess;
        Message = message;
        Errors = errors ?? Array.Empty<string>();
    }
}

public sealed class ServiceResult<T> : ServiceResult
{
    public T? Data { get; }

    private ServiceResult(bool isSuccess, T? data, string? message, IReadOnlyList<string>? errors)
        : base(isSuccess, message, errors)
    {
        Data = data;
    }

    public static ServiceResult<T> Success(T data, string? message = null)
        => new(true, data, message, null);

    public static ServiceResult<T> Fail(string message, params string[] errors)
        => new(false, default, message, errors);
}
