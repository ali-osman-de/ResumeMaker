using ResumeMaker.Domain.Entities.Base;
using ResumeMaker.Domain.Entities.Resume;

namespace ResumeMaker.Domain.Entities;

public class ResumeSum : CoreEntity
{
    public string ResumeTitle { get; set; }
    public string? Companies { get; set; }
    public Guid? PersonalDetailId { get; set; }
    public PersonalDetail? PersonalDetail { get; set; }
    public Guid? SummaryId { get; set; }
    public Summary? Summary { get; set; }
}
