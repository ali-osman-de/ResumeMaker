using ResumeMaker.Domain.Entities.Base;

namespace ResumeMaker.Domain.Entities.Certificates;

public class CertificatesInfo : CoreEntity
{
    public string Name { get; set; }
    public string Link { get; set; }
    public Guid ResumeId { get; set; }
    public Resume Resume { get; set; }
}
