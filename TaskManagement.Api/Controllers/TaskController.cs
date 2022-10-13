using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Controllers.Base;
using TaskManagement.Api.Requests.Tasks;
using TaskManagement.Application.Messages.Tasks;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TaskController : BaseController
    {
        public TaskController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<ActionResult> Get(int dailyListId, bool done, DateTime? deadlineLimit)
        {
            var userId = GetLoggedUserId();

            //TODO: Move request generation to factory classes for each controller
            var query = new GetTasksForDailyListQuery
            {
                UserId = userId,
                DailyListId = dailyListId,
                DeadlineLimit = deadlineLimit,
                Done = done
            };

            var result = await Mediator.Send(query);

            if (result.Success)
                return Ok(result.Value);

            return BadRequest(result.ErrorMessage);
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateTaskRequest createTaskRequest)
        {
            var userId = GetLoggedUserId();

            //TODO: Move request generation to factory classes for each controller
            var createTaskCommand = new CreateTaskCommand
            {
                UserId = userId,
                Deadline = createTaskRequest.Deadline,
                Title = createTaskRequest.Title,
                Description = createTaskRequest.Description,
                DailyListId = createTaskRequest.DailyListId
            };

            var result = await Mediator.Send(createTaskCommand);

            if (result.Success)
                return Ok(result.Value);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateTaskRequest updateTaskRequest)
        {
            var userId = GetLoggedUserId();

            //TODO: Move request generation to factory classes for each controller
            var updateTaskCommand = new UpdateTaskCommand
            {
                TaskId = id,
                UserId = userId,
                Deadline = updateTaskRequest.Deadline,
                Title = updateTaskRequest.Title,
                Description = updateTaskRequest.Description
            };

            var result = await Mediator.Send(updateTaskCommand);

            if (result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }

        [HttpPut("Done/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] bool done)
        {
            var userId = GetLoggedUserId();

            //TODO: Move request generation to factory classes for each controller
            var updateDoneStatusCommand = new UpdateTaskDoneStatusCommand
            {
                TaskId = id,
                UserId = userId,
                Done = done
            };

            var result = await Mediator.Send(updateDoneStatusCommand);

            if (result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }
    }
}