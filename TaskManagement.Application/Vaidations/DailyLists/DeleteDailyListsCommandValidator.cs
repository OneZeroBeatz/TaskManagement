using FluentValidation;
using TaskManagement.Application.Messages.DailyLists;

namespace TaskManagement.Application.Vaidations.DailyLists;

public class DeleteDailyListCommandValidator : AbstractValidator<DeleteDailyListCommand>
{
    public DeleteDailyListCommandValidator()
    {
        RuleFor(command => command).NotNull();
    }
}
