using MediatR;
using ResumeMaker.Application.Common;
using ResumeMaker.Application.Features.Commands.Resume;
using ResumeMaker.Application.Interfaces;

namespace ResumeMaker.Application.Features.Handlers.Resume;

public class CreateResumeCommandHandler : IRequestHandler<CreateResumeCommand, ServiceResult<bool>>
{
    private readonly IResumeService _resumeService;

    public CreateResumeCommandHandler(IResumeService resumeService)
    {
        _resumeService = resumeService;
    }

    public async Task<ServiceResult<bool>> Handle(CreateResumeCommand request, CancellationToken cancellationToken)
    {
        var result = await _resumeService.AddResumeSub(request.ResumeTitle, cancellationToken);
        return result;
    }
}
