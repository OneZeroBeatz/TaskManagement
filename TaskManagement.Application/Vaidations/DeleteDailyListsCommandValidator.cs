using FluentValidation;
using TaskManagement.Application.Messages.DailyLists;

namespace TaskManagement.Application.Vaidations
{
    public class DeleteDailyListCommandValidator : AbstractValidator<DeleteDailyListCommand>
    {
        public DeleteDailyListCommandValidator()
        {
        }
    }
}
