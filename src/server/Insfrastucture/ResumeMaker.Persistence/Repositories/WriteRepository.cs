using Microsoft.EntityFrameworkCore;
using ResumeMaker.Application.Interfaces.Repositories;
using ResumeMaker.Domain.Entities.Base;
using ResumeMaker.Persistence.Context;

namespace ResumeMaker.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : CoreEntity
{
    private readonly ResumeMakerDbContext _context;

    public WriteRepository(ResumeMakerDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<bool> AddAsync(T model, CancellationToken cancellationToken) 
    {
        await Table.AddAsync(model, cancellationToken);
        return true;
    }

    public async Task<bool> AddRangeAsync(List<T> datas, CancellationToken cancellationToken)
    {
        await Table.AddRangeAsync(datas, cancellationToken);
        return true;
    }

    public bool Delete(T model)
    {
        Table.Remove(model);
        return true;
    }

    public async Task<bool> RemoveById(Guid id, CancellationToken cancellationToken)
    {
        var entity = await Table.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity != null)
        {
            Table.Remove(entity);
            return true;
        }
        return false;
    }

    public bool RemoveRange(List<T> datas)
    {
        Table.RemoveRange(datas);
        return true;
    }

    public bool Update(T model)
    {
        Table.Update(model);
        return true;
    }
}
