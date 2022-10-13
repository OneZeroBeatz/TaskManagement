using FluentValidation;
using TaskManagement.Application.Messages;

namespace TaskManagement.Application.Vaidations
{
    public class CreateDailyListCommandValidator : AbstractValidator<CreateDailyListCommand>
    {
        public CreateDailyListCommandValidator()
        {
            RuleFor(command => command.Date).NotNull();
            RuleFor(command => command.Title).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(command => command.Description).NotNull().NotEmpty().MaximumLength(500);
        }
    }
}
