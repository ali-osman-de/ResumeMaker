using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Application.Interfaces.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : CoreEntity
{
    Task<bool> AddAsync(T model, CancellationToken cancellationToken);
    Task<bool> AddRangeAsync(List<T> datas, CancellationToken cancellationToken);
    bool Delete(T model);
    Task<bool> RemoveById(Guid id, CancellationToken cancellationToken);
    bool RemoveRange(List<T> datas);
    bool Update(T model);
}
