using FluentValidation;
using TaskManagement.Application.Messages.Tasks;

namespace TaskManagement.Application.Vaidations.Tasks;

public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskCommandValidator()
    {
        RuleFor(command => command).NotNull();
    }
}
