using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Controllers.Base;
using TaskManagement.Api.Requests;
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
        public async Task<ActionResult<string>> UpdateTimezone([FromBody] UpdateTimezoneRequest updateTimezonRequest)
        {
            var loggedUserEmail = GetLoggedUserEmail();

            //TODO: Move request generation to factory classes for each controller
            var updateTimezoneCommand = new UpdateTimezoneCommand
            {
                UserEmail = loggedUserEmail,
                TimeZoneId = updateTimezonRequest.TimeZoneId
            };

            var result = await Mediator.Send(updateTimezoneCommand);

            if(result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        } 
    }
}