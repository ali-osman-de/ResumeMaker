using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeMaker.API.Extensions;
using ResumeMaker.Application.Features.Commands.Resume;

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
        public async Task<IActionResult> CreateResume([FromBody] ResumeCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToResult();
        }   
        [HttpPut]
        public async Task<IActionResult> UpdateResume([FromBody] ResumeUpdateCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToResult();
        }   
        [HttpDelete]
        public async Task<IActionResult> RemoveResume([FromBody] ResumeRemoveCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToResult();
        }   

    }
}
