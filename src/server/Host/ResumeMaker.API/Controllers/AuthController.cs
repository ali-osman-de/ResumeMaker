using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResumeMaker.Application.Common;
using ResumeMaker.Application.Features.Commands;
using ResumeMaker.Application.Features.Commands.User;
using ResumeMaker.Domain.Dtos;

namespace ResumeMaker.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }   

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResult<TokenInformationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result); 
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] UpdateUserRefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
