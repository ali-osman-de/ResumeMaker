using MediatR;
using ResumeMaker.Application.Features.Commands;
using ResumeMaker.Application.Interfaces;

namespace ResumeMaker.Application.Features.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ServiceResult>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<ServiceResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.CreateUserAsync(request.userName, request.email, request.password, cancellationToken);
    }
}
