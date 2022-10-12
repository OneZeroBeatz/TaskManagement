using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Controllers.Base;
using TaskManagement.Application.Messages;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator) { }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> UpdateTimezone(UpdateTimezoneCommand updateTimezoneCommand)
        {
            //TODO: Separate Controller Action parameter and mediatR commands
            var loggedUserEmail = GetLoggedUserEmail();

            updateTimezoneCommand.Email = loggedUserEmail;

            var result = await Mediator.Send(updateTimezoneCommand);

            if(result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        } 
    }
}