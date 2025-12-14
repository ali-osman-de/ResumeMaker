using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ResumeMaker.Persistence.Context;

public class ResumeMakerDbContextFactory : IDesignTimeDbContextFactory<ResumeMakerDbContext>
{
    public ResumeMakerDbContext CreateDbContext(string[] args)
    {


        var optionsBuilder = new DbContextOptionsBuilder<ResumeMakerDbContext>()    
                                 .UseSqlServer("Server=localhost,1453;Database=ResumeMakerDb;User Id=SA;Password=Str0ng_Pass!;TrustServerCertificate=True;");
                                 
        return new ResumeMakerDbContext(optionsBuilder.Options);
    }
}
