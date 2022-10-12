using MediatR;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages
{
    public class UpdateDailyListCommand : IRequest<Result>
    {
        public int DailyListId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}