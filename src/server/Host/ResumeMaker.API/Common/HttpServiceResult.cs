using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResumeMaker.API.Common;

public sealed class HttpServiceResult<T> : ServiceResult<T>
{
    private HttpServiceResult(ServiceResult<T> result)
    {
        IsSuccess = result.IsSuccess;
        Message = result.Message;
        Errors = result.Errors;
        Data = result.Data;
    }

    public ActionResult<ServiceResult<T>> ToResult()
    {
        var statusCode = IsSuccess
            ? StatusCodes.Status200OK
            : StatusCodes.Status400BadRequest;

        return new ObjectResult(this)
        {
            StatusCode = statusCode
        };
    }

    public static HttpServiceResult<T> From(ServiceResult<T> result)
        => new(result);
}

public static class ServiceResultExtensions
{
    public static ActionResult<ServiceResult<T>> ToResult<T>(this ServiceResult<T> result)
    {
        if (result is null)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        return HttpServiceResult<T>.From(result).ToResult();
    }
}
