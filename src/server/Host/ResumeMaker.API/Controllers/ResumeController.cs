using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeMaker.Application.Features.Commands.Resume;
using ResumeMaker.Application.Features.Queries;

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
        public async Task<IActionResult> CreateResumeSub([FromBody] CreateResumeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllResume()
        {
            var result = await _mediator.Send(new GetAllResumeQuery());
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateResume([FromBody] UpdateResumeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveResume([FromQuery] string ResumeSubId)
        {
            var result = await _mediator.Send(new RemoveResumeCommand(ResumeSubId));
            return Ok(result);
        }
    }
}
