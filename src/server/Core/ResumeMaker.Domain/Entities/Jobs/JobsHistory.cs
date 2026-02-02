using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Domain.Entities.Jobs;

public class JobsHistory : CoreEntity
{
    public string Role { get; set; }
    public string StartEndPeriod { get; set; }
    public string Company { get; set; }
    public Guid ResumeId { get; set; }
    public Resume Resume { get; set; }
    public ICollection<JobDescription> JobDescriptions { get; set; } = new List<JobDescription>();
}
