using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Domain.Entities.Resume;

public class PersonalDetail : CoreEntity
{
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? GithubLink { get; set; }
    public string? LinkedinLink { get; set; }
    public Guid ResumeSumId { get; set; }
    public ResumeSum ResumeSum { get; set; }
}
