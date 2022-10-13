using FluentValidation;
using TaskManagement.Application.Messages;

namespace TaskManagement.Application.Vaidations
{
    public class UpdateDailyListCommandValidator : AbstractValidator<UpdateDailyListCommand>
    {
        public UpdateDailyListCommandValidator()
        {
            RuleFor(command => command.UserId).NotNull().NotEmpty();
            RuleFor(command => command.Date).NotNull();
            RuleFor(command => command.Title).NotNull();
            RuleFor(command => command.Description).NotNull();
        }
    }
}
