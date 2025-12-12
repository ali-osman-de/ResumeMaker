using Microsoft.EntityFrameworkCore;
using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Application.Interfaces.Repositories;

public interface IRepository<T> where T : CoreEntity
{
    DbSet<T> Table { get; }
}
