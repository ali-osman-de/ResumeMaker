namespace ResumeMaker.Application.Common;

public enum ResultStatus
{
    Success,
    NotFound,
    Unauthorized,
    ValidationError,
    Conflict,
    Error
}

public class ServiceResult
{
    public bool IsSuccess { get; }
    public ResultStatus Status { get; }
    public string? Message { get; }
    public IReadOnlyList<string> Errors { get; }

    protected ServiceResult(bool isSuccess, ResultStatus status, string? message, IReadOnlyList<string>? errors)
    {
        IsSuccess = isSuccess;
        Status = status;
        Message = message;
        Errors = errors ?? Array.Empty<string>();
    }
}

public sealed class ServiceResult<T> : ServiceResult
{
    public T? Data { get; }

    private ServiceResult(bool isSuccess, T? data, ResultStatus status, string? message, IReadOnlyList<string>? errors)
        : base(isSuccess, status, message, errors)
    {
        Data = data;
    }

    public static ServiceResult<T> Success(T data, string? message = null)
        => new(true, data, ResultStatus.Success, message, null);

    public static ServiceResult<T> Fail(ResultStatus status, string message, params string[] errors)
        => new(false, default, status, message, errors);
}
