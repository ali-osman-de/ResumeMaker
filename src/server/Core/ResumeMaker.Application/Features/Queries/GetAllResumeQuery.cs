using MediatR;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Queries;

public class GetAllResumeQuery : IRequest<ServiceResult<List<ResumeDto>>>;
