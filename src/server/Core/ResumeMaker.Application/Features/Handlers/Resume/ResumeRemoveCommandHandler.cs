using MediatR;
using ResumeMaker.Application.Features.Commands.Resume;
using ResumeMaker.Application.Interfaces;

namespace ResumeMaker.Application.Features.Commands.User;

public class ResumeRemoveCommandHandler(IResumeService resumeService) : IRequestHandler<ResumeRemoveCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(ResumeRemoveCommand request, CancellationToken cancellationToken)
    {
        return await resumeService.RemoveResume(request.resumeId, cancellationToken);
    }
}
