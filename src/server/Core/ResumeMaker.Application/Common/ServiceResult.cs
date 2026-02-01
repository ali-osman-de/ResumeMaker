using System.Net;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

public class ServiceResult
{
    [JsonIgnore]
    public HttpStatusCode Status { get; set; }
    public ProblemDetails? Fail { get; set; }

    [JsonIgnore]
    public bool isSuccess => Fail is null;
    
    [JsonIgnore]
    public bool isFail => !isSuccess;


    public static ServiceResult SuccessAsNoContent()
    {
        return new ServiceResult
        {
            Status = HttpStatusCode.NoContent
        };
    }
    public static ServiceResult ErrorAsNotFound()
    {
        return new ServiceResult
        {
            Status = HttpStatusCode.NotFound,
            Fail = new ProblemDetails
            {
                Title = "Not Found",
                Detail = "Source was not found!"
            }
        };
    }
    public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
    {
        return new ServiceResult()
        {
            Fail = problemDetails,
            Status = statusCode
        };

    }
    public static ServiceResult Error(string title, string description, HttpStatusCode statusCode)
    {
        return new ServiceResult()
        {
            Fail = new ProblemDetails
            {
                Title = title,
                Detail = description,
                Status = statusCode.GetHashCode()
            },
            Status = statusCode
        };

    }
    public static ServiceResult ErrorFromValidaton(Dictionary<string, object?> errors)
    {
        return new ServiceResult()
        {
            Fail = new ProblemDetails
            {
                Title = "Validation error occured",
                Detail = "please check the errors",
                Extensions = errors,
                Status = HttpStatusCode.BadRequest.GetHashCode()
            },
            Status = HttpStatusCode.BadRequest
        };

    }

}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; set; }
    public static ServiceResult<T> SuccessAsOk(T data)
    {
        return new ServiceResult<T>
        {
            Status = HttpStatusCode.OK,
            Data = data
        };
    }
    public static ServiceResult<T> SuccessAsCreated(T data)
    {
        return new ServiceResult<T>
        {
            Status = HttpStatusCode.OK,
            Data = data
        };
    }
    public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
    {
        return new ServiceResult<T>()
        {
            Fail = problemDetails,
            Status = statusCode
        };
        
    }
    public new static ServiceResult<T> Error(string title, string description, HttpStatusCode statusCode)
    {
        return new ServiceResult<T>()
        {
            Fail = new ProblemDetails
            {
                Title = title,
                Detail = description,
                Status = statusCode.GetHashCode()
            },
            Status = statusCode
        };

    }

}