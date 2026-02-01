using Microsoft.AspNetCore.Identity;

namespace ResumeMaker.Domain.Entities;

public class AppUser : IdentityUser<string>
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
    
}
