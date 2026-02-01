namespace ResumeMaker.Domain.Dtos.Section;

public class SkillCategoryDto
{
    public string Name { get; set; }
    public List<SkillDto> Skills { get; set; } = new();
}


