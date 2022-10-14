using FluentValidation;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Vaidations.Tasks;

public class UpdateTaskDoneStatusCommandValidator : AbstractValidator<UpdateTaskDoneStatusCommand>
{
    private readonly IDailyListRepository _dailyListRepository;
    private readonly ITaskRepository _taskRepository;

    public UpdateTaskDoneStatusCommandValidator(IDailyListRepository dailyListRepository, ITaskRepository taskRepository)
    {
        _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));

        RuleFor(command => command).NotNull();
        RuleFor(command => command)
            .MustAsync(ExistsForUser)
            .WithMessage("Task does not exist");
    }

    //TODO: Share logic with DeleteTaskCommandValidator
    public async Task<bool> ExistsForUser(UpdateTaskDoneStatusCommand command, CancellationToken token)
    {
        var task = await _taskRepository.FindAsync(command.TaskId, token);

        if (task == null)
            return false;

        var dailyListExists = await _dailyListRepository.Exists(task.DailyListId, command.UserId, token);

        if (!dailyListExists)
            return false;

        return true;
    }
}