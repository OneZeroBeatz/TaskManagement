using FluentValidation;
using TaskManagement.Application.Messages;

namespace TaskManagement.Application.Vaidations
{
    public class CreateDailyListCommandValidator : AbstractValidator<CreateDailyListCommand>
    {
        public CreateDailyListCommandValidator()
        {
            RuleFor(command => command.UserId).NotNull().NotEmpty();
            RuleFor(command => command.Date).NotNull();
            RuleFor(command => command.Title).NotNull();
            RuleFor(command => command.Description).NotNull();
        }
    }
}
