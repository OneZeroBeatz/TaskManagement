using FluentValidation;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Application.Vaidations.Tasks.Base;

namespace TaskManagement.Application.Vaidations.Tasks;

public class UpdateTaskCommandValidator : TaskPersistenceAbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator(IDailyListRepository dailyListRepository, ITaskRepository taskRepository)
        : base(dailyListRepository, taskRepository)
    {
        RuleFor(command => command).NotNull();
        RuleFor(command => command.Deadline).NotNull();
        RuleFor(command => command.Title).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(command => command.Description).NotNull().NotEmpty().MaximumLength(500);
        RuleFor(command => command)
            .MustAsync((command, token) => ExistsForUser(command.TaskId, command.UserId, token))
            .WithMessage("Task does not exist");
    }
}
