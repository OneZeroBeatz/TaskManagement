using FluentValidation;
using TaskManagement.Application.Messages;

namespace TaskManagement.Application.Vaidations
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
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
}
