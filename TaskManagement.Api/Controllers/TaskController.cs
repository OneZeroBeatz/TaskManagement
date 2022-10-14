using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Controllers.Base;
using TaskManagement.Api.Requests.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.Tasks;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TaskController : BaseController
    {
        public TaskController(IMediator mediator, ICurrentUserService currentUserService) : base(mediator, currentUserService) { }

        /// <summary>
        /// Fatching all tasks for one daily list that pass the filtering
        /// </summary>
        /// <param name="dailyListId">Daily list which tasks should be returned</param>
        /// <param name="done">If the task is finished or not</param>
        /// <param name="deadlineLimit">Tasks with deadlines before provided data will be returned</param>
        /// <returns>Tasks for daily list</returns>
        [HttpGet]
        public async Task<ActionResult> Get(int dailyListId, bool done, DateTime? deadlineLimit)
        {
            //TODO: Move request generation to factory classes for each controller
            var query = new GetTasksForDailyListQuery
            {
                UserId = CurrentUser.UserId!.Value,
                DailyListId = dailyListId,
                DeadlineLimit = deadlineLimit,
                Done = done
            };

            var result = await Mediator.Send(query);

            if (result.Success)
                return Ok(result.Value);

            return BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Creating task for signed user using provided data
        /// </summary>
        /// <param name="createTaskRequest">Request that contains data needed for creation</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateTaskRequest createTaskRequest)
        {
            //TODO: Move request generation to factory classes for each controller
            var createTaskCommand = new CreateTaskCommand
            {
                UserId = CurrentUser.UserId!.Value,
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

        /// <summary>
        /// Updating task for signed user using provided data
        /// </summary>
        /// <param name="id">Id of task that should be updated</param>
        /// <param name="updateTaskRequest">Request that contains data needed for update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateTaskRequest updateTaskRequest)
        {
            //TODO: Move request generation to factory classes for each controller
            var updateTaskCommand = new UpdateTaskCommand
            {
                TaskId = id,
                UserId = CurrentUser.UserId!.Value,
                Deadline = updateTaskRequest.Deadline,
                Title = updateTaskRequest.Title,
                Description = updateTaskRequest.Description
            };

            var result = await Mediator.Send(updateTaskCommand);

            if (result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Update done status of task.
        /// </summary>
        /// <param name="id">Id of task that should be updated</param>
        /// <param name="updateTaskDoneStatusRequest">Request that contains data needed for update</param>
        /// <returns></returns>
        [HttpPut("Done/{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateTaskDoneStatusRequest updateTaskDoneStatusRequest)
        {
            //TODO: Move request generation to factory classes for each controller
            var updateDoneStatusCommand = new UpdateTaskDoneStatusCommand
            {
                TaskId = id,
                UserId = CurrentUser.UserId!.Value,
                Done = updateTaskDoneStatusRequest.Done
            };

            var result = await Mediator.Send(updateDoneStatusCommand);

            if (result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }
        /// <summary>
        /// Deleting tasks for signed user and provided id
        /// </summary>
        /// <param name="id">Id of task that should be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            //TODO: Move request generation to factory classes for each controller
            var deleteDailyListCommand = new DeleteTaskCommand
            {
                TaskId = id,
                UserId = CurrentUser.UserId!.Value
            };

            var result = await Mediator.Send(deleteDailyListCommand);

            if (result.Success)
                return Ok();

            return BadRequest(result.ErrorMessage);
        }
    }
}