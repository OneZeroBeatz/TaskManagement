using FluentValidation;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Application.Vaidations.Tasks.Base;

namespace TaskManagement.Application.Vaidations.Tasks;

public class UpdateTaskDoneStatusCommandValidator : TaskPersistenceAbstractValidator<UpdateTaskDoneStatusCommand>
{
    public UpdateTaskDoneStatusCommandValidator(IDailyListRepository dailyListRepository, ITaskRepository taskRepository)
        : base(dailyListRepository, taskRepository)
    {
        RuleFor(command => command).NotNull();
        RuleFor(command => command)
            .MustAsync((command, token) => ExistsForUser(command.TaskId, command.TaskId, token))
            .WithMessage("Task does not exist");
    }
}