using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ResumeMaker.Application.Interfaces;
using ResumeMaker.Application.Interfaces.Repositories;
using ResumeMaker.Application.Interfaces.UOW;
using ResumeMaker.Domain.Dtos;
using ResumeMaker.Domain.Entities;

namespace ResumeMaker.Application.Services;

public class ResumeService : IResumeService
{
    private readonly IWriteRepository<Resume> _resumeWriteRepository;
    private readonly IReadRepository<Resume> _resumeReadRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public ResumeService(
        IWriteRepository<Resume> resumeWriteRepository,
        IReadRepository<Resume> resumeReadRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _resumeWriteRepository = resumeWriteRepository;
        _resumeReadRepository = resumeReadRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult> CreateResume(SaveResumeDto saveResumeDto, CancellationToken cancellationToken)
    {
        var resume = _mapper.Map<Resume>(saveResumeDto);
        var userId = _currentUserService.UserId;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return ServiceResult.Error("Unauthorized", "Kullanıcı bulunamadı.", HttpStatusCode.Unauthorized);
        }

        resume.AppUserId = userId;

        var added = await _resumeWriteRepository.AddAsync(resume, cancellationToken);
        if (!added)
        {
            return ServiceResult.Error("Bad Request", "Resume kaydedilemedi.", HttpStatusCode.BadRequest);
        }

        var saved = await _unitOfWork.SaveChangesAsync();
        if (saved == 0)
        {
            return ServiceResult.Error("Internal Server Error", "Resume kaydedilemedi.", HttpStatusCode.InternalServerError);
        }

        return ServiceResult.SuccessAsNoContent();
    }

    public async Task<ServiceResult<List<ResumeSummaryDto>>> GetAllResumeByUserId(string userId, CancellationToken cancellationToken)
    {
        var resumes = _resumeReadRepository.Table.AsNoTracking()
            .Where(x => x.AppUserId == userId)
            .ToList();
        var mappedResumes = _mapper.Map<List<ResumeSummaryDto>>(resumes);
        return ServiceResult<List<ResumeSummaryDto>>.SuccessAsOk(mappedResumes);
    }

    public async Task<ServiceResult<SaveResumeDto>> GetResumeByResumeId(Guid resumeId, CancellationToken cancellationToken)
    {
        var resume = await _resumeReadRepository.Table.FirstOrDefaultAsync(x => x.Id == resumeId);
        var mappedResume = _mapper.Map<SaveResumeDto>(resume);
        return ServiceResult<SaveResumeDto>.SuccessAsOk(mappedResume);
    }

    public async Task<ServiceResult> RemoveResume(Guid resumeId, CancellationToken cancellationToken)
    {
        var removed = await _resumeWriteRepository.RemoveById(resumeId, cancellationToken);
        if (!removed)
        {
            return ServiceResult.Error("Not Found", "Resume bulunamadı.", HttpStatusCode.NotFound);
        }

        var saved = await _unitOfWork.SaveChangesAsync();
        if (saved == 0)
        {
            return ServiceResult.Error("Internal Server Error", "Resume silinemedi.", HttpStatusCode.InternalServerError);
        }

        return ServiceResult.SuccessAsNoContent();
    }

    public async Task<ServiceResult<SaveResumeDto>> UpdateResume(Guid resumeId, SaveResumeDto saveResumeDto, CancellationToken cancellationToken)
    {
        var resume = await _resumeWriteRepository.Table
            .Include(x => x.SkillCategories)
            .ThenInclude(x => x.TechnologyHubs)
            .Include(y => y.JobsHistories)
            .ThenInclude(y => y.JobDescriptions)
            .Include(z => z.VolunteerInfos)
            .ThenInclude(z => z.VolunteerDescriptions)
            .Include(l => l.ProjectsInfos)
            .ThenInclude(l => l.ProjectDefinitions)
            .FirstOrDefaultAsync(x => x.Id == resumeId, cancellationToken);

        if (resume == null)
        {
            return ServiceResult<SaveResumeDto>.Error("Not Found", "Resume bulunamadı.", HttpStatusCode.NotFound);
        }

        _mapper.Map(saveResumeDto, resume);

        var updated = _resumeWriteRepository.Update(resume);
        if (!updated)
        {
            return ServiceResult<SaveResumeDto>.Error("Bad Request", "Resume güncellenemedi.", HttpStatusCode.BadRequest);
        }

        var saved = await _unitOfWork.SaveChangesAsync();
        if (saved == 0)
        {
            return ServiceResult<SaveResumeDto>.Error("Internal Server Error", "Resume güncellenemedi.", HttpStatusCode.InternalServerError);
        }

        var resultDto = _mapper.Map<SaveResumeDto>(resume);
        return ServiceResult<SaveResumeDto>.SuccessAsOk(resultDto);
    }
}
