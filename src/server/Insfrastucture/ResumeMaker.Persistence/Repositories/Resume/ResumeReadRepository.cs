using ResumeMaker.Application.Interfaces.Repositories;
using ResumeMaker.Domain.Entities;
using ResumeMaker.Persistence.Context;

namespace ResumeMaker.Persistence.Repositories.Resume;

public class ResumeReadRepository : ReadRepository<ResumeSum>, IResumeReadRepository
{
    public ResumeReadRepository(ResumeMakerDbContext context) : base(context)
    {
    }
}
