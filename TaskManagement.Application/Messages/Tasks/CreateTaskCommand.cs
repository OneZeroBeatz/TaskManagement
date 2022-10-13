using MediatR;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages.Tasks
{
    public class CreateTaskCommand : IRequest<Result<int>>
    {
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public int DailyListId { get; set; }
    }
}