using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Domain.Entities.Volunteers;

public class VolunteerInfo : CoreEntity
{
    public string Role { get; set; }
    public string Company { get; set; }
    public int Year { get; set; }
    public Guid ResumeId { get; set; }
    public Resume Resume { get; set; }
    public ICollection<VolunteerDescription>? VolunteerDescriptions { get; set; }
}
