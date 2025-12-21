using MediatR;
using ResumeMaker.Application.Features.Commands.Resume;
using ResumeMaker.Application.Interfaces;

namespace ResumeMaker.Application.Features.Handlers.Resume;

public class UpdateResumeCommandHandler : IRequestHandler<UpdateResumeCommand, ServiceResult<bool>>
{
    private readonly IResumeService _resumeService;

    public UpdateResumeCommandHandler(IResumeService resumeService)
    {
        _resumeService = resumeService;
    }

    public async Task<ServiceResult<bool>> Handle(UpdateResumeCommand request, CancellationToken cancellationToken)
    {
        var result = await _resumeService.UpdateResumeSub(request.ResumeSubId, request.ResumeTitle, request.Companies, cancellationToken);
        return result;
    }
}
