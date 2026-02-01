using System.Net;
using Microsoft.AspNetCore.Mvc;
namespace ResumeMaker.API.Extensions;

public static class EndpointResultExtension
{
    public static IActionResult ToResult<T>(this ServiceResult<T> result)
    {
        return result.Status switch
        {
            HttpStatusCode.OK => new OkObjectResult(result.Data),
            HttpStatusCode.Created => new CreatedResult(),
            HttpStatusCode.NoContent => new NoContentResult(),
            HttpStatusCode.BadRequest => new BadRequestObjectResult(result.Fail),
            HttpStatusCode.NotFound => new NotFoundObjectResult(result.Fail),
            _ => new ObjectResult(result.Fail) { StatusCode = (int)result.Status }
        };
    }

    public static IActionResult ToResult(this ServiceResult result)
    {
        return result.Status switch
        {
            HttpStatusCode.OK => new OkResult(),
            HttpStatusCode.NoContent => new NoContentResult(),
            HttpStatusCode.NotFound => new NotFoundObjectResult(result.Fail),
            HttpStatusCode.BadRequest => new BadRequestObjectResult(result.Fail),
            _ => new ObjectResult(result.Fail) { StatusCode = (int)result.Status }
        };
    }
}
