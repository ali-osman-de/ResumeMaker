
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Interfaces.Token;

public interface ITokenService
{
    TokenInformationDto CreateAccessToken();
    string CreateRefreshToken();
}
