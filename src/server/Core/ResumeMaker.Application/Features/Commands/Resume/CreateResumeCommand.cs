using MediatR;
using ResumeMaker.Application.Common;

namespace ResumeMaker.Application.Features.Commands.Resume;

public sealed record CreateResumeCommand(string ResumeTitle) : IRequest<ServiceResult<bool>>;