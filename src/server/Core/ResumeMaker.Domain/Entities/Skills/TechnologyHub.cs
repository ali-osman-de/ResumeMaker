using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Domain.Entities.Skills;

public class TechnologyHub : CoreEntity
{
    public string Technology { get; set; }
    public string Level { get; set; }
    public Guid SkillCategoryId { get; set; }
    public SkillCategory? SkillCategory { get; set; }
}
