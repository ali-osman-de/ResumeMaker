using System.Text.Json.Serialization;
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
    public string AppUserId { get; set; }
    public ICollection<SkillCategory> SkillCategories { get; set; } = new List<SkillCategory>();
    public ICollection<JobsHistory> JobsHistories { get; set; } = new List<JobsHistory>();
    public ICollection<EducationInfo> EducationInfos { get; set; } = new List<EducationInfo>();
    public ICollection<ProjectsInfo> ProjectsInfos { get; set; } = new List<ProjectsInfo>();
    public ICollection<VolunteerInfo> VolunteerInfos { get; set; } = new List<VolunteerInfo>();
    public ICollection<CertificatesInfo> CertificatesInfos { get; set; } = new List<CertificatesInfo>();
}
