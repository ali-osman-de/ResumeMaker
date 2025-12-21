using MediatR;

namespace ResumeMaker.Application.Features.Commands;

public sealed record CreateUserCommand(string userName, string email, string password) : IRequest<ServiceResult<bool>>;