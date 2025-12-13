using ResumeMaker.Application.Common;

namespace ResumeMaker.Application.Interfaces;

public interface IResumeService
{
    Task<ServiceResult<bool>> AddResumeSub(string ResumeTitle, CancellationToken cancellationToken);
}
