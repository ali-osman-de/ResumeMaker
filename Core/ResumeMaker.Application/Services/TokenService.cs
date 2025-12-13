using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ResumeMaker.Application.Interfaces.Token;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenInformationDto CreateAccessToken()
    {
        TokenInformationDto  tokenInformationDto = new TokenInformationDto();

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]!));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        tokenInformationDto.ExpirationDatetime = DateTime.UtcNow.AddSeconds(15);
        JwtSecurityToken jwtSecurityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: tokenInformationDto.ExpirationDatetime,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler securityTokenHandler = new();
        tokenInformationDto.AccessToken = securityTokenHandler.WriteToken(jwtSecurityToken);

        tokenInformationDto.RefreshToken = CreateRefreshToken();

        return tokenInformationDto;
    }

    public string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator numberGenerator =  RandomNumberGenerator.Create();
        numberGenerator.GetBytes(number);
        return Convert.ToBase64String(number);
    }
}
