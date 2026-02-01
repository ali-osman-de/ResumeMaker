using MediatR;
using ResumeMaker.Application.Features.Commands.Resume;
using ResumeMaker.Application.Interfaces;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Handlers.Resume;

public class ResumeUpdateCommandHandler(IResumeService resumeService) : IRequestHandler<ResumeUpdateCommand, ServiceResult<SaveResumeDto>>
{
    public async Task<ServiceResult<SaveResumeDto>> Handle(ResumeUpdateCommand request, CancellationToken cancellationToken)
    {
        return await resumeService.UpdateResume(request.resumeId, request.SaveResumeDto, cancellationToken);
    }
}
