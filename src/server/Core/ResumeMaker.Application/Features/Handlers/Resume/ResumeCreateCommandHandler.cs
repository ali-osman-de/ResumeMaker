using MediatR;
using ResumeMaker.Application.Features.Commands.Resume;
using ResumeMaker.Application.Interfaces;

namespace ResumeMaker.Application.Features.Handlers.Resume;

public class ResumeCreateCommandHandler(IResumeService resumeService) : IRequestHandler<ResumeCreateCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(ResumeCreateCommand request, CancellationToken cancellationToken)
    {
        return await resumeService.CreateResume(request.SaveResumeDto, cancellationToken);
    }
}
