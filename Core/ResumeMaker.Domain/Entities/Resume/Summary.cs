using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Domain.Entities.Resume;

public class Summary : CoreEntity
{
    public string SummaryResume { get; set; }
    public Guid ResumeSumId { get; set; }
    public ResumeSum ResumeSum { get; set; }
}
