using Microsoft.EntityFrameworkCore;
using ResumeMaker.Application.Interfaces.Repositories;
using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : CoreEntity
{
    public DbSet<T> Table => throw new NotImplementedException();

    public Task<bool> AddAsync(T model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddRangeAsync(List<T> datas, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public bool Delete(T model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public bool RemoveRange(List<T> datas)
    {
        throw new NotImplementedException();
    }

    public bool Update(T model)
    {
        throw new NotImplementedException();
    }
}
