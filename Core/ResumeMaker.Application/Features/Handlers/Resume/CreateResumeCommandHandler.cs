using MediatR;
using ResumeMaker.Application.Common;
using ResumeMaker.Application.Features.Commands.Resume;

namespace ResumeMaker.Application.Features.Handlers.Resume;

public class CreateResumeCommandHandler : IRequestHandler<CreateResumeCommand, ServiceResult<bool>>
{
    public Task<ServiceResult<bool>> Handle(CreateResumeCommand request, CancellationToken cancellationToken)
    {
        
    }
}
