using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Controllers.Base;
using TaskManagement.Api.Requests.DailyLists;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.DailyLists;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DailyListController : BaseController
    {
        public DailyListController(IMediator mediator, ICurrentUserService currentUserService) : base(mediator, currentUserService) { }

        /// <summary>
        /// Fatching one page of configurable size containing daily lists filtered by provided parameter for signed user
        /// </summary>
        /// <param name="date">Date for which the list is created</param>
        /// <param name="title">Title of the list, will be filtered using contains method, so not full title must be provided</param>
        /// <param name="page">Page number</param>
        /// <returns>Daily lists page</returns>
        [HttpGet]
        public async Task<ActionResult> Get(DateTime? date, string? title, int page)
        {
            //TODO: Move request generation to factory classes for each controller
            var query = new GetDailyListsQuery
            {
                Date = date,
                Title = string.IsNullOrEmpty(title) ? string.Empty : title,
                UserId = CurrentUser.UserId!.Value,
                Page = page,
            };

            var result = await Mediator.Send(query);

            if (result.Success)
                return Ok(result.Value);

            return BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Creating daily list for signed user using provided data
        /// </summary>
        /// <param name="createDailyListRequest">Request that contains data needed for creation</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateDailyListRequest createDailyListRequest)
        {
            //TODO: Move request generation to factory classes for each controller
            var createDailyListCommand = new CreateDailyListCommand
            {
                UserId = CurrentUser.UserId!.Value,
                Date = createDailyListRequest.Date,
                Title = createDailyListRequest.Title,
                Description = createDailyListRequest.Description,
            };

            var result = await Mediator.Send(createDailyListCommand);

            if (result.Success)
                return Ok(result.Value);

            return BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Updating daily list for signed user using provided data
        /// </summary>
        /// <param name="id">Id of daily list that should be updated</param>
        /// <param name="updateDailyListRequest">Request that contains data needed for update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateDailyListRequest updateDailyListRequest)
        {
            //TODO: Move request generation to factory classes for each controller
            var updateDailyListCommand = new UpdateDailyListCommand
            {
                Title = updateDailyListRequest.Title,
                Date = updateDailyListRequest.Date,
                Description = updateDailyListRequest.Description,
                DailyListId = id,
                UserId = CurrentUser.UserId!.Value
            };

            var result = await Mediator.Send(updateDailyListCommand);

            if (result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Deleting daily list for signed user and provided id
        /// </summary>
        /// <param name="id">Id of daily list that should be deleted</param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            //TODO: Move request generation to factory classes for each controller
            var deleteDailyListCommand = new DeleteDailyListCommand
            {
                DailyListId = id,
                UserId = CurrentUser.UserId!.Value
            };

            var result = await Mediator.Send(deleteDailyListCommand);

            if (result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }
    }
}