using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Interfaces;

public interface IResumeService
{
    Task<ServiceResult> CreateResume(SaveResumeDto saveResumeDto, CancellationToken cancellationToken);
    Task<ServiceResult> RemoveResume(Guid resumeId, CancellationToken cancellationToken);
    Task<ServiceResult<SaveResumeDto>> UpdateResume(Guid resumeId, SaveResumeDto saveResumeDto, CancellationToken cancellationToken);
    Task<ServiceResult<List<SaveResumeDto>>> GetAllResumeByUserId(Guid userId, CancellationToken cancellationToken);
    Task<ServiceResult<SaveResumeDto>> GetResumeByResumeId(Guid resumeId, CancellationToken cancellationToken);
}
