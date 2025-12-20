using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResumeMaker.Application.Common;

namespace ResumeMaker.API.Filters;

public class ServiceResultActionFilter : IAsyncResultFilter
{
    public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult && objectResult.Value is ServiceResult serviceResult)
        {
            context.Result = Map(serviceResult);
        }
        else if (context.Result is ServiceResult directResult)
        {
            context.Result = Map(directResult);
        }

        return next();
    }

    private static IActionResult Map(ServiceResult result)
    {
        return result.Status switch
        {
            ResultStatus.Success => new OkObjectResult(result),
            ResultStatus.NotFound => new NotFoundObjectResult(result),
            ResultStatus.Unauthorized => new UnauthorizedObjectResult(result),
            ResultStatus.ValidationError => new BadRequestObjectResult(result),
            ResultStatus.Conflict => new ConflictObjectResult(result),
            _ => new ObjectResult(result) { StatusCode = StatusCodes.Status500InternalServerError }
        };
    }
}
