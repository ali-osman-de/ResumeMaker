using MediatR;
using ResumeMaker.Application.Common;
using ResumeMaker.Application.Features.Queries;
using ResumeMaker.Application.Interfaces;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Handlers.Resume;

public class GetAllResumeQueryHandler : IRequestHandler<GetAllResumeQuery, ServiceResult<List<ResumeDto>>>
{
    private readonly IResumeService _resumeService;

    public GetAllResumeQueryHandler(IResumeService resumeService)
    {
        _resumeService = resumeService;
    }

    public async Task<ServiceResult<List<ResumeDto>>> Handle(GetAllResumeQuery request, CancellationToken cancellationToken)
    {
        var result = await _resumeService.GetAllResumeSub(cancellationToken);
        return result;
    }
}
