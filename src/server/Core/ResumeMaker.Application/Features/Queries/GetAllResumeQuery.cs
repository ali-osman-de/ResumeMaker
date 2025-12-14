using MediatR;
using ResumeMaker.Application.Common;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Queries;

public class GetAllResumeQuery : IRequest<ServiceResult<List<ResumeDto>>>;
