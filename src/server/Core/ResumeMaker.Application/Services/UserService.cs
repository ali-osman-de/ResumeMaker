using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ResumeMaker.Application.Interfaces;
using ResumeMaker.Domain.Dtos;
using ResumeMaker.Domain.Entities;

namespace ResumeMaker.Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly Interfaces.Token.ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, Interfaces.Token.ITokenService tokenService, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<ServiceResult> CreateUserAsync(string userName, string email, string password, CancellationToken cancellationToken)
    {
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = userName,
            Email = email
        }, password);

        if (result.Succeeded)
        {
            return ServiceResult.SuccessAsNoContent();
        }

        return ServiceResult.Error("Conflict", "Kullanıcı username veya email kullanımda!", HttpStatusCode.Conflict);
    }

    public async Task<ServiceResult<TokenInformationDto>> LoginUserAsync(string userNameOrEmail, string password, CancellationToken cancellationToken)
    {
        AppUser? appUser = await _userManager.FindByEmailAsync(userNameOrEmail);
        if (appUser == null) appUser = await _userManager.FindByNameAsync(userNameOrEmail);

        if (appUser == null)
        {
            return ServiceResult<TokenInformationDto>.Error("Not Found", "Kullanıcı bulunamadı!", HttpStatusCode.NotFound);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(appUser, password, false);

        if (result.Succeeded)
        {
            TokenInformationDto tokenInformationDto = _tokenService.CreateAccessToken(appUser);
            await UpdateRefreshToken(tokenInformationDto.RefreshToken, appUser, tokenInformationDto.ExpirationDatetime, 15);
            return ServiceResult<TokenInformationDto>.SuccessAsOk(tokenInformationDto);
        }

        return ServiceResult<TokenInformationDto>.Error("Unauthorized", "hatalı şifre veya username", HttpStatusCode.Unauthorized);
    }

    public async Task<ServiceResult<RefreshTokenInformationDto>> RenewRefreshToken(string refreshToken, CancellationToken cancellation)
    {
        AppUser? appUser =  await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        if (appUser != null && appUser?.RefreshTokenExpiration > DateTime.UtcNow)
        {
            RefreshTokenInformationDto refreshTokenInformationDto = new RefreshTokenInformationDto();
            TokenInformationDto tokenInformationDto = _tokenService.CreateAccessToken(appUser);
            await UpdateRefreshToken(tokenInformationDto.RefreshToken, appUser, tokenInformationDto.ExpirationDatetime, double.Parse(_configuration["Token:AccessTokenExpiration"]!));
            refreshTokenInformationDto.RefreshToken = tokenInformationDto.RefreshToken;
            return ServiceResult<RefreshTokenInformationDto>.SuccessAsOk(refreshTokenInformationDto);
        }

        return ServiceResult<RefreshTokenInformationDto>.Error("Unauthorized", "UserBulunamadı veya Refresh Token süresi dolmuş", HttpStatusCode.Unauthorized);
    }

    public async Task UpdateRefreshToken(string refreshToken, AppUser appUser, DateTime accessTokenDate, double addOnAccessTokenDate)
    {
        if (appUser != null) {

            appUser.RefreshToken = refreshToken;
            appUser.RefreshTokenExpiration = accessTokenDate.AddSeconds(addOnAccessTokenDate);
            await _userManager.UpdateAsync(appUser);
        }
        else
        {
            throw new Exception("Refresh Token oluştururken hata oluştur");
        }
    }
}
