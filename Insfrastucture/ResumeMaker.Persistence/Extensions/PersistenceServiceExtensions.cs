using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumeMaker.Domain.Entities;
using ResumeMaker.Persistence.Context;

namespace ResumeMaker.Persistence.Extensions;

public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ResumeMakerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultDocker")));
        
        services.AddIdentityCore<AppUser>().AddRoles<AppRole>()
                                           .AddSignInManager<SignInManager<AppUser>>()
                                           .AddEntityFrameworkStores<ResumeMakerDbContext>();

        services.AddSingleton<TimeProvider>(TimeProvider.System);

        return services;
    }
}
