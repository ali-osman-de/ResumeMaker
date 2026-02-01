using AutoMapper;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<TokenInformationDto, RefreshTokenInformationDto>();
    }
}
