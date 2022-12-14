using MediatR;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages.DailyLists;

public record CreateDailyListCommand : IRequest<Result<int>>
{
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}