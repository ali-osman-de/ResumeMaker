using MediatR;
using ResumeMaker.Application.Common;

namespace ResumeMaker.Application.Features.Commands;

public sealed record CreateUserCommand(string userName, string email, string password) : IServiceResultWrapper.IRequestByServiceResult;
