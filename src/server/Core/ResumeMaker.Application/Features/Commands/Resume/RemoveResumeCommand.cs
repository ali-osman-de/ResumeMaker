using MediatR;

namespace ResumeMaker.Application.Features.Commands.Resume;

public sealed record RemoveResumeCommand(string ResumeSubId) : IRequest<ServiceResult<bool>>;
