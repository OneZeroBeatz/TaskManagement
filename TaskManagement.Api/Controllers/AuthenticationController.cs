using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Controllers.Base;
using TaskManagement.Application.Messages;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {

        public AuthenticationController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginCommand loginCommand)
        {
            var result = await Mediator.Send(loginCommand);

            if(result.Success)
                return Ok(result.Value);

            return BadRequest(result.ErrorMessage);
        } 
    }
}