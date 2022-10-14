using FluentValidation;
using TaskManagement.Application.Messages.Users;

namespace TaskManagement.Application.Vaidations.Users;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command);
        RuleFor(command => command.Password)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(command => command.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(50);
    }
}
