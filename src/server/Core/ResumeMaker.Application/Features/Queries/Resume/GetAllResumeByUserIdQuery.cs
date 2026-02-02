using ResumeMaker.Application.Common;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Queries.Resume;

public sealed record GetAllResumeByUserIdQuery(Guid userId) : IServiceResultWrapper.IRequestByServiceResult<List<SaveResumeDto>>;
