using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Interfaces;

public interface IResumeService
{
    Task<ServiceResult<List<ResumeDto>>> GetAllResumeSub(CancellationToken cancellationToken);
    Task<ServiceResult<bool>> UpdateResumeSub(string ResumeSubId, string ResumeTitle, string Companies, CancellationToken cancellationToken);
    Task<ServiceResult<bool>> AddResumeSub(string ResumeTitle, CancellationToken cancellationToken);
    Task<ServiceResult<bool>> RemoveResumeSub(string ResumeSubId, CancellationToken cancellationToken);
}
