using ResumeMaker.Application.Common;

namespace ResumeMaker.Application.Features.Commands.Resume;

public sealed record ResumeRemoveCommand(Guid resumeId) : IServiceResultWrapper.IRequestByServiceResult;
