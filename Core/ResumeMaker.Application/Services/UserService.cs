using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResumeMaker.Application.Common;
using ResumeMaker.Application.Interfaces;
using ResumeMaker.Domain.Dtos;
using ResumeMaker.Domain.Entities;

namespace ResumeMaker.Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly Interfaces.Token.ITokenService _tokenService;
    public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, Interfaces.Token.ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<ServiceResult<bool>> CreateUserAsync(string userName, string email, string password, CancellationToken cancellationToken)
    {
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = userName,
            Email = email
        }, password);

        if (result.Succeeded)
        {
            return ServiceResult<bool>.Success(true);
        }

        return ServiceResult<bool>.Success(false);
    }

    public async Task<ServiceResult<TokenInformationDto>> LoginUserAsync(string userNameOrEmail, string password, CancellationToken cancellationToken)
    {
        AppUser? appUser = await _userManager.FindByEmailAsync(userNameOrEmail);
        if (appUser == null) appUser = await _userManager.FindByNameAsync(userNameOrEmail);

        if (appUser == null) return ServiceResult<TokenInformationDto>.Fail("Kullanıcı bulunamadı!");

        var result = await _signInManager.CheckPasswordSignInAsync(appUser, password, false);

        if (result.Succeeded)
        {
            TokenInformationDto tokenInformationDto = _tokenService.CreateAccessToken(appUser);
            await UpdateRefreshToken(tokenInformationDto.RefreshToken, appUser, tokenInformationDto.ExpirationDatetime, 15);
            return ServiceResult<TokenInformationDto>.Success(tokenInformationDto, "Giriş başarılı");
        }

        return ServiceResult<TokenInformationDto>.Fail("hatalı şifre veya username");
    }

    public async Task<ServiceResult<RefreshTokenInformationDto>> RenewRefreshToken(string refreshToken, CancellationToken cancellation)
    {
        AppUser? appUser =  await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        if (appUser != null && appUser?.RefreshTokenExpiration > DateTime.UtcNow)
        {
            RefreshTokenInformationDto refreshTokenInformationDto = new RefreshTokenInformationDto();
            TokenInformationDto tokenInformationDto = _tokenService.CreateAccessToken(appUser);
            await UpdateRefreshToken(tokenInformationDto.RefreshToken, appUser, tokenInformationDto.ExpirationDatetime, 15);
            refreshTokenInformationDto.RefreshToken = tokenInformationDto.RefreshToken;
            return ServiceResult<RefreshTokenInformationDto>.Success(refreshTokenInformationDto);
        }
        else
        {
            return ServiceResult<RefreshTokenInformationDto>.Fail("UserBulunamadı veya Refresh Token süresi dolmuş");
        }
    }

    public async Task UpdateRefreshToken(string refreshToken, AppUser appUser, DateTime accessTokenDate, int addOnAccessTokenDate)
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
