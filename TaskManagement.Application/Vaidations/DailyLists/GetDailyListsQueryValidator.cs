using FluentValidation;
using TaskManagement.Application.Messages.DailyLists;

namespace TaskManagement.Application.Vaidations.DailyLists;

public class GetDailyListsQueryValidator : AbstractValidator<GetDailyListsQuery>
{
    public GetDailyListsQueryValidator()
    {
        RuleFor(command => command).NotNull();
        RuleFor(command => command.Page).GreaterThan(0);
        RuleFor(command => command.Date).NotNull();
        RuleFor(command => command.Title).NotNull();
    }
}
