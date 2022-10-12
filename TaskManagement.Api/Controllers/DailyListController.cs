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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DailyListController : BaseController
    {
        public DailyListController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult> Get(DateTime? startDate, string? title, int page)
        {
            var loggedUserEmail = GetLoggedUserEmail();

            //TODO: Move request generation to factory classes for each controller
            var query = new GetDailyListsQuery
            {
                Date = startDate,
                Title = string.IsNullOrEmpty(title) ? string.Empty : title,
                UserEmail = loggedUserEmail,
                Page = page,
            };

            var result = await Mediator.Send(query);

            if (result.Success)
                return Ok(result.Value);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateDailyListCommand createDailyListCommand)
        {
            //TODO: Separate Controller Action parameter class and mediatR commands
            var loggedUserEmail = GetLoggedUserEmail();

            createDailyListCommand.UserEmail = loggedUserEmail;

            var result = await Mediator.Send(createDailyListCommand);

            if (result.Success)
                return Ok(result.Value);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateDailyListCommand updateDailyListCommand)
        {
            //TODO: Separate Controller Action parameter class and mediatR commands
            var loggedUserEmail = GetLoggedUserEmail();

            updateDailyListCommand.UserEmail = loggedUserEmail;
            updateDailyListCommand.DailyListId = id;

            var result = await Mediator.Send(updateDailyListCommand);

            if (result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //TODO: Separate Controller Action parameter class and mediatR commands
            var loggedUserEmail = GetLoggedUserEmail();

            var deleteDailyListCommand = new DeleteDailyListCommand
            {
                DailyListId = id,
                UserEmail = loggedUserEmail
            };

            var result = await Mediator.Send(deleteDailyListCommand);

            if (result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }
    }
}