using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using TaskManagement.Application.Messages;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> UpdateTimezone(UpdateTimezoneCommand updateTimezoneCommand)
        {
            //TODO: Separate Controller Action parameter and mediatR commands
            var claimIdentity = HttpContext.User.Identity as ClaimsIdentity;

            if (claimIdentity == null)
                return Unauthorized();

            var claim = claimIdentity.FindFirst(ClaimTypes.Email);

            if (claim == null)
                return Unauthorized();

            updateTimezoneCommand.Email = claim.Value;

            var result = await _mediator.Send(updateTimezoneCommand);

            if(result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        } 
    }
}