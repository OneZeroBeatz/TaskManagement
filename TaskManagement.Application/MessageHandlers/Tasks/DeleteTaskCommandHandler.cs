using FluentValidation;
using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Shared;

namespace TaskManagement.Application.MessageHandlers.Tasks;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result>
{
    private readonly IDailyListRepository _dailyListRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IValidator<DeleteTaskCommand> _validator;

    public DeleteTaskCommandHandler(IValidator<DeleteTaskCommand> validator,
                                    IDailyListRepository dailyListRepository,
                                    ITaskRepository taskRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    }

    public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(request);
        if (!result.IsValid)
            return result.CreateErrorResult();

        var task = await _taskRepository.FindAsync(request.TaskId);
        if (task == null)
            return Result.Error("Task does not exist.");

        var dailyListExists = await _dailyListRepository.Exists(task.DailyListId, request.UserId, cancellationToken);

        if (!dailyListExists)
            return Result.Error<int>("Not allowed to update specified task.");

        await _taskRepository.DeleteAsync(request.TaskId);

        return Result.Ok();
    }
}
