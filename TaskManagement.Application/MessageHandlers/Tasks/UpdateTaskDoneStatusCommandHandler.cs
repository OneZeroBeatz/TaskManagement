using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.Tasks;

public class UpdateTaskDoneStatusCommandHandler : IRequestHandler<UpdateTaskDoneStatusCommand, Result>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IDailyListRepository _dailyListRepository;
    private readonly IValidator<UpdateTaskDoneStatusCommand> _validator;

    public UpdateTaskDoneStatusCommandHandler(IValidator<UpdateTaskDoneStatusCommand> validator,
                                              IDailyListRepository dailyListRepository,
                                              ITaskRepository taskRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    }

    public async Task<Result> Handle(UpdateTaskDoneStatusCommand request, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(request);
        if (!result.IsValid)
            return result.CreateErrorResult();

        var task = await _taskRepository.FindAsync(request.TaskId);

        if (task == null)
            return Result.Error("Task does not exist.");

        var listExists = await _dailyListRepository.Exists(task.DailyListId, request.UserId, cancellationToken);

        if (!listExists)
            return Result.Error<int>("Not allowed to update specified task.");

        task.Done = request.Done;
        task.LastDoneUpdate = DateTime.UtcNow.Date;

        await _taskRepository.UpdateAsync(task);

        return Result.Ok();
    }
}
