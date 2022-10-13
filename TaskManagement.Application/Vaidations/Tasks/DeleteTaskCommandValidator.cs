using FluentValidation;
using TaskManagement.Application.Messages.Tasks;

namespace TaskManagement.Application.Vaidations
{
    public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskCommandValidator()
        {
            RuleFor(command => command).NotNull();
        }
    }
}
