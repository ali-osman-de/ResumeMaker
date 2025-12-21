using MediatR;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Commands.User;

public sealed record UpdateUserRefreshTokenCommand(string RefreshToken) : IRequest<ServiceResult<RefreshTokenInformationDto>>;
