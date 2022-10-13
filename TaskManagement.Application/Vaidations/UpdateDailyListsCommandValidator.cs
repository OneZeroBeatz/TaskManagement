using FluentValidation;
using TaskManagement.Application.Messages;

namespace TaskManagement.Application.Vaidations
{
    public class UpdateDailyListCommandValidator : AbstractValidator<UpdateDailyListCommand>
    {
        public UpdateDailyListCommandValidator()
        {
            RuleFor(command => command.Date).NotNull();
            RuleFor(command => command.Title).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(command => command.Description).NotNull().NotEmpty().MaximumLength(500);
        }
    }
}
