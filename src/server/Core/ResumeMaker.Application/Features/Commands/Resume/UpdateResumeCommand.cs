using MediatR;

namespace ResumeMaker.Application.Features.Commands.Resume;

public sealed record UpdateResumeCommand(string ResumeSubId, string ResumeTitle, string Companies) : IRequest<ServiceResult<bool>>;
