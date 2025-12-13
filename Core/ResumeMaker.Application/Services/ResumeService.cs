using ResumeMaker.Application.Common;
using ResumeMaker.Application.Interfaces;
using ResumeMaker.Application.Interfaces.Repositories;
using ResumeMaker.Application.Interfaces.UOW;
using ResumeMaker.Domain.Entities;

namespace ResumeMaker.Application.Services;

public class ResumeService : IResumeService
{
    private readonly IResumeWriteRepository _resumeWriteRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;
    public ResumeService(IResumeWriteRepository resumeWriteRepository,
                         IUnitOfWork unitOfWork,
                         ICurrentUserService currentUser)
    {
        _resumeWriteRepository = resumeWriteRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<ServiceResult<bool>> AddResumeSub(string ResumeTitle, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUser.UserId))
        {
            return ServiceResult<bool>.Fail("Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.");
        }

        ResumeSum resumeSum = new ResumeSum
        {
            ResumeTitle = ResumeTitle,
            AppUserId = _currentUser.UserId
        };

        await _resumeWriteRepository.AddAsync(resumeSum, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<bool>.Success(true);
    }
}
