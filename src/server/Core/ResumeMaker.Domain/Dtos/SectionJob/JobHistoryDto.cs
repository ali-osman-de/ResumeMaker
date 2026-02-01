namespace ResumeMaker.Domain.Dtos.SectionJob;

public class JobHistoryDto
{
    public string Role { get; set; }
    public string StartEndPeriod { get; set; }
    public string Company { get; set; }
    public List<JobDescriptionDto> JobDescriptionDtos { get; set; } = new();
}
