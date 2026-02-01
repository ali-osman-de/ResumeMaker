using ResumeMaker.Application.Common;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.Application.Features.Commands.Resume;
//TODO burada ilerleyen safhalarda client kısmında neresi değiştiyse o kısmı alacak şekilde güncellenecek
public sealed record ResumeUpdateCommand(Guid resumeId, SaveResumeDto SaveResumeDto) : IServiceResultWrapper.IRequestByServiceResult<SaveResumeDto>;
