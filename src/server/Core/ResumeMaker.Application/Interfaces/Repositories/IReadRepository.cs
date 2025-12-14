using ResumeMaker.Domain.Entities.Base;
using System.Linq.Expressions;

namespace ResumeMaker.Application.Interfaces.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : CoreEntity
{
    Task<List<T>> GetAll(CancellationToken cancellationToken);
    List<T> GetWhere(Expression<Func<T, bool>> method, CancellationToken cancellationToken);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> method, CancellationToken cancellationToken);
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
