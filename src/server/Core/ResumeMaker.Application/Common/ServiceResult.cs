public class ServiceResult<T>
{
    public bool IsSuccess { get; init; }
    public string? Message { get; init; }
    public IReadOnlyList<string> Errors { get; init; } = Array.Empty<string>();
    public T? Data { get; init; }

    public static ServiceResult<T> Success(T? data = default, string? message = null)
        => new ServiceResult<T>
        {
            IsSuccess = true,
            Data = data,
            Message = message
        };

    public static ServiceResult<T> Fail(string? message = null, params string[] errors)
        => new ServiceResult<T>
        {
            IsSuccess = false,
            Message = message,
            Errors = errors?.Length > 0 ? errors : Array.Empty<string>()
        };
}
