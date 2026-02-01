using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResumeMaker.Domain.Entities;

namespace ResumeMaker.Persistence.Context;

public class ResumeMakerDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public ResumeMakerDbContext(DbContextOptions<ResumeMakerDbContext> options) : base(options)
    {
    }

}
