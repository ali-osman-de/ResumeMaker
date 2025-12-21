using MediatR;
using ResumeMaker.Application.Features.Commands.Resume;
using ResumeMaker.Application.Interfaces;

namespace ResumeMaker.Application.Features.Handlers.Resume;

public class RemoveResumeCommandHandler : IRequestHandler<RemoveResumeCommand, ServiceResult<bool>>
{
    private readonly IResumeService _resumeService;

    public RemoveResumeCommandHandler(IResumeService resumeService)
    {
        _resumeService = resumeService;
    }

    public async Task<ServiceResult<bool>> Handle(RemoveResumeCommand request, CancellationToken cancellationToken)
    {
        var result = await _resumeService.RemoveResumeSub(request.ResumeSubId, cancellationToken);
        return result;
    }
}
