using Microsoft.EntityFrameworkCore;
using ResumeMaker.Application.Interfaces.Repositories;
using ResumeMaker.Domain.Entities.Base;
using System.Linq.Expressions;

namespace ResumeMaker.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : CoreEntity
{
    public DbSet<T> Table => throw new NotImplementedException();

    public Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetSingleAsync(Expression<Func<T, bool>> method, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public List<T> GetWhere(Expression<Func<T, bool>> method)
    {
        throw new NotImplementedException();
    }
}
