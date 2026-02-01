using ResumeMaker.Application.Common;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Commands.User;

public sealed record LoginUserCommand(string userNameOrEmail, string password) : IServiceResultWrapper.IRequestByServiceResult<TokenInformationDto>;
