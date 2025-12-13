using ResumeMaker.Application.Common;
using ResumeMaker.Domain.Dtos;
using ResumeMaker.Domain.Entities;

namespace ResumeMaker.Application.Interfaces;

public interface IUserService
{
    Task<ServiceResult<bool>> CreateUserAsync(string userName, string email, string password, CancellationToken cancellationToken);
    Task<ServiceResult<TokenInformationDto>> LoginUserAsync(string userNameOrEmail, string password, CancellationToken cancellationToken);
    
    Task UpdateRefreshToken(string refreshToken, AppUser appUser, DateTime accessTokenDate, int addOnAccessTokenDate);
    Task<ServiceResult<RefreshTokenInformationDto>> RenewRefreshToken(string refreshToken, CancellationToken cancellation);
}
