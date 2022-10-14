using MediatR;
using TaskManagement.Application.Messages.Responses;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages.Tasks;

public record GetTasksForDailyListQuery : IRequest<Result<GetTasksForDailyListResponse>>
{
    public int UserId { get; set; }
    public int DailyListId { get; set; }
    public DateTime? DeadlineLimit { get; set; }
    public bool Done { get; set; }
}