using MediatR;
using ResumeMaker.Application.Features.Queries.Resume;
using ResumeMaker.Application.Interfaces;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Handlers.Resume;

public class GetResumeByResumeIdQueryHandler(IResumeService resumeService) : IRequestHandler<GetResumeByResumeIdQuery, ServiceResult<SaveResumeDto>>
{
    public async Task<ServiceResult<SaveResumeDto>> Handle(GetResumeByResumeIdQuery request, CancellationToken cancellationToken)
    {
        return await resumeService.GetResumeByResumeId(request.resumeId, cancellationToken);
    }
}
