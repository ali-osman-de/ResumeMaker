using ResumeMaker.Application.Interfaces;
using ResumeMaker.Application.Interfaces.Repositories;
using ResumeMaker.Application.Interfaces.UOW;
using ResumeMaker.Domain.Dtos;
using ResumeMaker.Domain.Entities;

namespace ResumeMaker.Application.Services;

public class ResumeService : IResumeService
{
    private readonly IResumeWriteRepository _resumeWriteRepository;
    private readonly IResumeReadRepository _resumeReadRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;
    public ResumeService(IResumeWriteRepository resumeWriteRepository,
                         IUnitOfWork unitOfWork,
                         ICurrentUserService currentUser,
                         IResumeReadRepository resumeReadRepository)
    {
        _resumeWriteRepository = resumeWriteRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _resumeReadRepository = resumeReadRepository;
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

    public async Task<ServiceResult<List<ResumeDto>>> GetAllResumeSub(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUser.UserId))
        {
            return ServiceResult<List<ResumeDto>>.Fail("Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.");
        }

        var resumes = _resumeReadRepository.GetWhere(r => r.AppUserId == _currentUser.UserId, cancellationToken);
        var dtoList = resumes.Select(r => new ResumeDto
        {
            Id = r.Id,
            ResumeTitle = r.ResumeTitle,
            Companies = r.Companies ?? string.Empty
        }).ToList();

        return ServiceResult<List<ResumeDto>>.Success(dtoList);
    }

    public async Task<ServiceResult<bool>> RemoveResumeSub(string ResumeSubId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUser.UserId))
        {
            return ServiceResult<bool>.Fail("Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.");
        }

        if (!Guid.TryParse(ResumeSubId, out var resumeId))
        {
            return ServiceResult<bool>.Fail("Geçersiz özgeçmiş kimliği.");
        }

        var resume = _resumeReadRepository.GetWhere(r => r.Id == resumeId && r.AppUserId == _currentUser.UserId, cancellationToken).FirstOrDefault();
        if (resume is null)
        {
            return ServiceResult<bool>.Fail("Özgeçmiş bulunamadı.");
        }

        _resumeWriteRepository.Delete(resume);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<bool>> UpdateResumeSub(string ResumeSubId, string ResumeTitle, string Companies, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_currentUser.UserId))
        {
            return ServiceResult<bool>.Fail("Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.");
        }

        if (!Guid.TryParse(ResumeSubId, out var resumeId))
        {
            return ServiceResult<bool>.Fail("Geçersiz özgeçmiş kimliği.");
        }

        var resume = _resumeReadRepository.GetWhere(r => r.Id == resumeId && r.AppUserId == _currentUser.UserId, cancellationToken).FirstOrDefault();
        if (resume is null)
        {
            return ServiceResult<bool>.Fail("Özgeçmiş bulunamadı.");
        }

        resume.UpdatedAt = DateTime.UtcNow;
        resume.ResumeTitle = ResumeTitle;
        resume.Companies = Companies;
        _resumeWriteRepository.Update(resume);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<bool>.Success(true);
    }
}
