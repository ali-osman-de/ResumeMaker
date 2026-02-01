using MediatR;
using ResumeMaker.Application.Features.Commands.User;
using ResumeMaker.Application.Interfaces;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Handlers;

public class UpdateUserRefreshTokenCommandHandler : IRequestHandler<UpdateUserRefreshTokenCommand, ServiceResult<RefreshTokenInformationDto>>
{
    private readonly IUserService _userService;

    public UpdateUserRefreshTokenCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<ServiceResult<RefreshTokenInformationDto>> Handle(UpdateUserRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _userService.RenewRefreshToken(request.RefreshToken, cancellationToken);
    }
}
