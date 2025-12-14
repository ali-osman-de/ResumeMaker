
using ResumeMaker.Domain.Dtos;
using ResumeMaker.Domain.Entities;

namespace ResumeMaker.Application.Interfaces.Token;

public interface ITokenService
{
    TokenInformationDto CreateAccessToken(AppUser appUser);
    string CreateRefreshToken();
}
