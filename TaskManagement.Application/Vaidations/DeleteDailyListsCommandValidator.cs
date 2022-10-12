using FluentValidation;
using TaskManagement.Application.Messages;

namespace TaskManagement.Application.Vaidations
{
    public class DeleteDailyListCommandValidator : AbstractValidator<DeleteDailyListCommand>
    {
        public DeleteDailyListCommandValidator()
        {
            RuleFor(command => command.UserEmail).NotNull().NotEmpty();
        }
    }
}
