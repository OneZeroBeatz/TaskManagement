using FluentValidation;
using TaskManagement.Application.Messages.Tasks;

namespace TaskManagement.Application.Vaidations.Tasks
{
    public class UpdateTaskDoneStatusCommandValidator : AbstractValidator<UpdateTaskDoneStatusCommand>
    {
        public UpdateTaskDoneStatusCommandValidator()
        {
            RuleFor(command => command).NotNull();
        }
    }
}
