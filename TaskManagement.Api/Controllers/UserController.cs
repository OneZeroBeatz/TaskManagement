using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Controllers.Base;
using TaskManagement.Api.Requests.Users;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.Users;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator, ICurrentUserService currentUserService) : base(mediator, currentUserService) { }

        /// <summary>
        /// Updating timezone for signed user
        /// </summary>
        /// <param name="updateTimezonRequest">Request that contains data needed for update</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> UpdateTimezone([FromBody] UpdateTimezoneRequest updateTimezonRequest)
        {
            var loggedUserId = GetLoggedUserId();

            //TODO: Move request generation to factory classes for each controller
            var updateTimezoneCommand = new UpdateTimezoneCommand
            {
                UserId = loggedUserId!.Value,
                TimeZoneId = updateTimezonRequest.TimezoneId
            };

            var result = await Mediator.Send(updateTimezoneCommand);

            if(result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        } 
    }
}