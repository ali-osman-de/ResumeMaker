using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Domain.Entities.Jobs;

public class JobDescription : CoreEntity
{
    public string Description { get; set; }
    public Guid JobsHistoryId { get; set; }
    public JobsHistory JobsHistory { get; set; }
}
