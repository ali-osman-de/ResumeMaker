using MediatR;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Commands.User;

public sealed record LoginUserCommand(string userNameOrEmail, string password) : IRequest<ServiceResult<TokenInformationDto>>;
