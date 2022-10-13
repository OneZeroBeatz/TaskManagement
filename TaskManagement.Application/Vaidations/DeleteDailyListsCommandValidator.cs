using FluentValidation;
using TaskManagement.Application.Messages;

namespace TaskManagement.Application.Vaidations
{
    public class DeleteDailyListCommandValidator : AbstractValidator<DeleteDailyListCommand>
    {
        public DeleteDailyListCommandValidator()
        {
        }
    }
}
