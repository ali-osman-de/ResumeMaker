namespace ResumeMaker.Domain.Dtos.SectionProjects;

public class ProjectsInfoDto
{
    public string Name { get; set; }
    public string Link { get; set; }
    public List<ProjectDefinitionsDto>? ProjectDefinitionsDtos { get; set; }
}
