using FluentValidation;
using TaskManagement.Application.Messages.Tasks;

namespace TaskManagement.Application.Vaidations.Tasks;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(command => command).NotNull();
        RuleFor(command => command.Deadline).NotNull();
        RuleFor(command => command.Title).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(command => command.Description).NotNull().NotEmpty().MaximumLength(500);
    }
}
