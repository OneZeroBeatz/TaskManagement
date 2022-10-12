using MediatR;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages
{
    public class DeleteDailyListCommand : IRequest<Result>
    {
        public int DailyListId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
    }
}