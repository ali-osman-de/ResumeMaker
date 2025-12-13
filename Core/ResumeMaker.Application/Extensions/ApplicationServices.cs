using Microsoft.Extensions.DependencyInjection;
using ResumeMaker.Application.Features.Commands.User;
using ResumeMaker.Application.Interfaces;
using ResumeMaker.Application.Interfaces.Token;
using ResumeMaker.Application.Services;

namespace ResumeMaker.Application.Extensions;

public static class ApplicationServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<LoginUserCommand>());

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
    }
}
