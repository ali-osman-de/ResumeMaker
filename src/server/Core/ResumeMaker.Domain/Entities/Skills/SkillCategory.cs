using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Domain.Entities.Skills;

public class SkillCategory : CoreEntity
{
    public string Name { get; set; }    
    public Guid ResumeId { get; set; }
    public Resume Resume { get; set; }
    public ICollection<TechnologyHub> TechnologyHubs { get; set; }
}
