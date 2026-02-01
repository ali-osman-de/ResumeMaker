namespace ResumeMaker.Domain.Dtos.SectionVolunteer;

public class VolunteerInfoDto
{
    public string Role { get; set; }
    public string Company { get; set; }
    public int Year { get; set; }
    public List<VolunteerDescriptionDto>? VolunteerDescriptionDtos { get; set; }
}
