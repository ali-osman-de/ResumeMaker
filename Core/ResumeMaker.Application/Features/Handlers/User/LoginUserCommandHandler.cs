using MediatR;
using ResumeMaker.Application.Common;
using ResumeMaker.Application.Features.Commands.User;
using ResumeMaker.Application.Interfaces;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Handlers;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ServiceResult<TokenInformationDto>>
{
    private readonly IUserService _userService;

    public LoginUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public Task<ServiceResult<TokenInformationDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = _userService.LoginUserAsync(request.userNameOrEmail, request.password, cancellationToken);
        return result;
    }
}
