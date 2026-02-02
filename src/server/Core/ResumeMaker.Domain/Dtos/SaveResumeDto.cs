using ResumeMaker.Domain.Dtos.Section;
using ResumeMaker.Domain.Dtos.SectionCertificate;
using ResumeMaker.Domain.Dtos.SectionEducation;
using ResumeMaker.Domain.Dtos.SectionJob;
using ResumeMaker.Domain.Dtos.SectionProjects;
using ResumeMaker.Domain.Dtos.SectionVolunteer;

namespace ResumeMaker.Domain.Dtos;

public class SaveResumeDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public int? CellPhone { get; set; }
    public string? Location { get; set; }
    public string? GithubLink { get; set; }
    public string? LinkedinLink { get; set; }
    public string? Autobiography { get; set; }
    public List<SkillCategoryDto> SkillCategoryDtos { get; set; } = new();
    public List<JobHistoryDto> JobHistoryDtos { get; set; } = new();
    public List<EducationInfoDto> EducationInfoDtos { get; set; } = new();
    public List<ProjectsInfoDto> ProjectsInfoDtos { get; set; } = new();
    public List<VolunteerInfoDto> VolunteerInfoDtos { get; set; } = new();
    public List<CertificatesInfoDto> CertificatesInfoDtos { get; set; } = new();
}
