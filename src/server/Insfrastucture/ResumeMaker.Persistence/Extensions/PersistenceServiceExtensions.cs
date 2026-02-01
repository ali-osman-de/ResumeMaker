using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumeMaker.Application.Interfaces.Repositories;
using ResumeMaker.Application.Interfaces.UOW;
using ResumeMaker.Domain.Entities;
using ResumeMaker.Persistence.Context;
using ResumeMaker.Persistence.Repositories;
using ResumeMaker.Persistence.Repositories.Uow;

namespace ResumeMaker.Persistence.Extensions;

public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ResumeMakerDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultDocker")));
        
        services.AddIdentityCore<AppUser>().AddRoles<AppRole>()
                                           .AddSignInManager<SignInManager<AppUser>>()
                                           .AddEntityFrameworkStores<ResumeMakerDbContext>();

        services.AddSingleton<TimeProvider>(TimeProvider.System);

        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}
