using MediatR;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages.DailyLists;

public record DeleteDailyListCommand : IRequest<Result>
{
    public int DailyListId { get; set; }
    public int UserId { get; set; }
}