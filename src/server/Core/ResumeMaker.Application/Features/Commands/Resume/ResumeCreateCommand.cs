using ResumeMaker.Application.Common;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Commands.Resume;

public sealed record ResumeCreateCommand(SaveResumeDto SaveResumeDto) : IServiceResultWrapper.IRequestByServiceResult;
