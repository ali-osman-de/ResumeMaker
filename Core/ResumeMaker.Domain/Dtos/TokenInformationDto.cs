namespace ResumeMaker.Domain.Dtos;

public class TokenInformationDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpirationDatetime { get; set; }
}
