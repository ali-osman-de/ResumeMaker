using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ResumeMaker.Application.Interfaces;

namespace ResumeMaker.Application.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _http;

    public CurrentUserService(IHttpContextAccessor http) => _http = http;

    public string? UserId =>
        _http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
