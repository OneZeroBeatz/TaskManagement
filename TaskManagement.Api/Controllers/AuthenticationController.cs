using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Controllers.Base;
using TaskManagement.Api.Requests.Users;
using TaskManagement.Application.Messages;
using TaskManagement.Application.Messages.Users;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
        {
            var loginCommand = new LoginCommand
            {
                Email = loginRequest.Email,
                Password = loginRequest.Password,
            };

            var result = await Mediator.Send(loginCommand);

            if(result.Success)
                return Ok(result.Value);

            return BadRequest(result.ErrorMessage);
        } 
    }
}