using FluentValidation;
using TaskManagement.Application.Messages.DailyLists;

namespace TaskManagement.Application.Vaidations.DailyLists;

public class UpdateDailyListCommandValidator : AbstractValidator<UpdateDailyListCommand>
{
    public UpdateDailyListCommandValidator()
    {
        RuleFor(command => command).NotNull();
        RuleFor(command => command.Date).NotNull();
        RuleFor(command => command.Title).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(command => command.Description).NotNull().NotEmpty().MaximumLength(500);
    }
}
