using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ResumeMaker.Application.Extensions;
using ResumeMaker.Persistence.Extensions;

namespace ResumeMaker.API.Extensions;

public static class HostServiceExtensions
{
    public static void AddHostServices(this IServiceCollection services, IConfiguration configuration, string swaggerUrl, string outputPath)
    {
        services.AddApplicationServices();
        services.AddPersistenceServices(configuration);

        services.AddSingleton<IMetadataGenerator>(
            new NswagMetadataGenerator(swaggerUrl, outputPath));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidAudience = configuration["Token:Audience"],
                ValidIssuer = configuration["Token:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]!)),
                LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
            };
        });
    }
}
