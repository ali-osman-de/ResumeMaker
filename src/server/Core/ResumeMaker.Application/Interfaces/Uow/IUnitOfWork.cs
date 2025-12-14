namespace ResumeMaker.Application.Interfaces.UOW;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
