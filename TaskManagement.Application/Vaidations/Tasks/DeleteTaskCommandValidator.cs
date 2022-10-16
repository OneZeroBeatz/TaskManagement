using FluentValidation;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using TaskManagement.Application.Vaidations.Tasks.Base;

namespace TaskManagement.Application.Vaidations.Tasks;

public class DeleteTaskCommandValidator : TaskPersistenceAbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskCommandValidator(IDailyListRepository dailyListRepository, ITaskRepository taskRepository) 
        : base(dailyListRepository, taskRepository)
    {

        RuleFor(command => command).NotNull();
        RuleFor(command => command)
            .MustAsync((command, token) => ExistsForUser(command.TaskId, command.UserId, token))
            .WithMessage("Task does not exist");
    }
}
