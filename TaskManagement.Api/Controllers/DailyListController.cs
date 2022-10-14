using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Controllers.Base;
using TaskManagement.Api.Requests.DailyLists;
using TaskManagement.Application.Messages;
using TaskManagement.Application.Messages.DailyLists;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DailyListController : BaseController
    {
        public DailyListController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<ActionResult> Get(DateTime? startDate, string? title, int page)
        {
            var userId = GetLoggedUserId();

            //TODO: Move request generation to factory classes for each controller
            var query = new GetDailyListsQuery
            {
                Date = startDate,
                Title = string.IsNullOrEmpty(title) ? string.Empty : title,
                UserId = userId,
                Page = page,
            };

            var result = await Mediator.Send(query);

            if (result.Success)
                return Ok(result.Value);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateDailyListRequest createDailyListRequest)
        {
            var userId = GetLoggedUserId();

            //TODO: Move request generation to factory classes for each controller
            var createDailyListCommand = new CreateDailyListCommand
            {
                UserId = userId,
                Date = createDailyListRequest.Date,
                Title = createDailyListRequest.Title,
                Description = createDailyListRequest.Description,
            };

            var result = await Mediator.Send(createDailyListCommand);

            if (result.Success)
                return Ok(result.Value);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateDailyListRequest updateDailyListRequest)
        {
            var loggedUserId = GetLoggedUserId();

            //TODO: Move request generation to factory classes for each controller
            var updateDailyListCommand = new UpdateDailyListCommand
            {
                Title = updateDailyListRequest.Title,
                Date = updateDailyListRequest.Date,
                Description = updateDailyListRequest.Description,
                DailyListId = id,
                UserId = loggedUserId
            };

            var result = await Mediator.Send(updateDailyListCommand);

            if (result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var loggedUserId = GetLoggedUserId();

            //TODO: Move request generation to factory classes for each controller
            var deleteDailyListCommand = new DeleteDailyListCommand
            {
                DailyListId = id,
                UserId = loggedUserId
            };

            var result = await Mediator.Send(deleteDailyListCommand);

            if (result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }
    }
}