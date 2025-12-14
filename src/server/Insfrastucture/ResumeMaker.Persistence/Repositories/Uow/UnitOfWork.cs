using Microsoft.EntityFrameworkCore;
using ResumeMaker.Application.Interfaces.UOW;
using ResumeMaker.Domain.Entities.Base;
using ResumeMaker.Persistence.Context;

namespace ResumeMaker.Persistence.Repositories.Uow;

public class UnitOfWork : IUnitOfWork
{
    private readonly ResumeMakerDbContext _context;

    public UnitOfWork(ResumeMakerDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
