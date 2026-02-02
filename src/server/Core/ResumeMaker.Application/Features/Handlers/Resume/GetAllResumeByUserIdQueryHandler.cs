using MediatR;
using ResumeMaker.Application.Features.Queries.Resume;
using ResumeMaker.Application.Interfaces;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Handlers.Resume;

public class GetAllResumeByUserIdQueryHandler(IResumeService resumeService) : IRequestHandler<GetAllResumeByUserIdQuery, ServiceResult<List<SaveResumeDto>>>
{
    public async Task<ServiceResult<List<SaveResumeDto>>> Handle(GetAllResumeByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await resumeService.GetAllResumeByUserId(request.userId, cancellationToken);
    }
}
