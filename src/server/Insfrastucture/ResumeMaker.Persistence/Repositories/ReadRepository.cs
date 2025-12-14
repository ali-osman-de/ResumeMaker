using Microsoft.EntityFrameworkCore;
using ResumeMaker.Application.Interfaces.Repositories;
using ResumeMaker.Domain.Entities.Base;
using ResumeMaker.Persistence.Context;
using System.Linq.Expressions;

namespace ResumeMaker.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : CoreEntity
{
    private readonly ResumeMakerDbContext _context;

    public ReadRepository(ResumeMakerDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<List<T>> GetAll(CancellationToken cancellationToken) 
        => await Table.ToListAsync(cancellationToken);

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
        => await Table.FirstAsync(data => data.Id == id, cancellationToken);

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, CancellationToken cancellationToken) 
        => await Table.AsQueryable().Where(method).FirstAsync(cancellationToken);

    public List<T> GetWhere(Expression<Func<T, bool>> method, CancellationToken cancellationToken)
        => Table.Where(method).ToList();
}
