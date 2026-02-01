using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    }
}
