using ResumeMaker.Application.Interfaces.Repositories;
using ResumeMaker.Domain.Entities;
using ResumeMaker.Persistence.Context;

namespace ResumeMaker.Persistence.Repositories.Resume;

public class ResumeWriteRepository : WriteRepository<ResumeSum>, IResumeWriteRepository
{
    public ResumeWriteRepository(ResumeMakerDbContext context) : base(context)
    {
    }

}
