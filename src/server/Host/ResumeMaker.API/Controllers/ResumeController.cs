using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeMaker.Application.Common;
using ResumeMaker.Application.Features.Commands.Resume;
using ResumeMaker.Application.Features.Queries;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ResumeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResumeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<bool>>> CreateResumeSub([FromBody] CreateResumeCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<ResumeDto>>>> GetAllResume()
        {
            var result = await _mediator.Send(new GetAllResumeQuery());
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResult<bool>>> UpdateResume([FromBody] UpdateResumeCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResult<bool>>> RemoveResume([FromQuery] string ResumeSubId)
        {
            var result = await _mediator.Send(new RemoveResumeCommand(ResumeSubId));
            return result;
        }
    }
}
