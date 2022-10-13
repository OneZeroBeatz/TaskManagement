using FluentValidation;
using TaskManagement.Application.Messages;

namespace TaskManagement.Application.Vaidations
{
    public class GetDailyListsQueryValidator : AbstractValidator<GetDailyListsQuery>
    {
        public GetDailyListsQueryValidator()
        {
            RuleFor(command => command.Page).GreaterThan(0);
            RuleFor(command => command.Date).NotNull();
            RuleFor(command => command.Title).NotNull();
        }
    }
}
