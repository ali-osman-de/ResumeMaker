using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Domain.Entities.Educations;

public class EducationInfo : CoreEntity
{
    public string ColleageName { get; set; }
    public string DepartmantAndMajor { get; set; }
    public string Grade { get; set; }
    public int? GPA { get; set; }
    public Guid ResumeId { get; set; }
    public Resume Resume { get; set; }
}
