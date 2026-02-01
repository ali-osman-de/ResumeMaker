using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Domain.Entities.Projects;

public class ProjectDefinitions : CoreEntity
{
    public string Explaining { get; set; }
    public Guid ProjectsInfoId { get; set; }
    public ProjectsInfo ProjectsInfo { get; set; }
}
