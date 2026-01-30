using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ResumeMaker.Persistence.Context;

public class ResumeMakerDbContextFactory : IDesignTimeDbContextFactory<ResumeMakerDbContext>
{
    public ResumeMakerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ResumeMakerDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=resumemaker;Username=resumemaker;Password=resumemaker;");

        return new ResumeMakerDbContext(optionsBuilder.Options);
    }
}
