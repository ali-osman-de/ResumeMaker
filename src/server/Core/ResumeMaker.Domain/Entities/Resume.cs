using ResumeMaker.Domain.Entities.Base;
using ResumeMaker.Domain.Entities.Certificates;
using ResumeMaker.Domain.Entities.Educations;
using ResumeMaker.Domain.Entities.Jobs;
using ResumeMaker.Domain.Entities.Projects;
using ResumeMaker.Domain.Entities.Skills;
using ResumeMaker.Domain.Entities.Volunteers;

namespace ResumeMaker.Domain.Entities;

public class Resume : CoreEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public int? CellPhone { get; set; }
    public string? Location { get; set; }
    public string? GithubLink { get; set; }
    public string? LinkedinLink { get; set; }
    public string? Autobiography { get; set; }
    public ICollection<SkillCategory>? SkillCategories { get; set; }
    public ICollection<JobsHistory>? JobsHistories { get; set; }
    public ICollection<EducationInfo>? EducationInfos { get; set; }
    public ICollection<ProjectsInfo>? ProjectsInfos { get; set; }
    public ICollection<VolunteerInfo>? VolunteerInfos { get; set; }
    public ICollection<CertificatesInfo>? CertificatesInfos { get; set; }
}
