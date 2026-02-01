using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Domain.Entities.Volunteers;

public class VolunteerDescription : CoreEntity
{
    public string Explaining { get; set; }
    public Guid VolunteerInfoId { get; set; }
    public VolunteerInfo VolunteerInfo { get; set; }
}
