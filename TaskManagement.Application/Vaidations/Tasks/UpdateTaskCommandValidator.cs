using FluentValidation;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Vaidations.Tasks;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    private readonly IDailyListRepository _dailyListRepository;
    private readonly ITaskRepository _taskRepository;

    public UpdateTaskCommandValidator(IDailyListRepository dailyListRepository, ITaskRepository taskRepository)
    {
        _dailyListRepository = dailyListRepository ?? throw new ArgumentNullException(nameof(dailyListRepository));
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));

        RuleFor(command => command).NotNull();
        RuleFor(command => command.Deadline).NotNull();
        RuleFor(command => command.Title).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(command => command.Description).NotNull().NotEmpty().MaximumLength(500);
        RuleFor(command => command)
            .MustAsync(ExistsForUser)
            .WithMessage("Task does not exist");
    }

    //TODO: Share logic with DeleteTaskCommandValidator
    public async Task<bool> ExistsForUser(UpdateTaskCommand command, CancellationToken token)
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
