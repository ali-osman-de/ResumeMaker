using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Domain.Entities.Projects;

public class ProjectsInfo : CoreEntity
{
    public string Name { get; set; }
    public string Link { get; set; }
    public Guid ResumeId { get; set; }
    public Resume Resume { get; set; }
    public ICollection<ProjectDefinitions> ProjectDefinitions { get; set; }
}
