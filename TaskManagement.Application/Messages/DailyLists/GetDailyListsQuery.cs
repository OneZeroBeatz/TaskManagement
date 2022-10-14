using MediatR;
using TaskManagement.Application.Messages.Responses;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages.DailyLists;

public class GetDailyListsQuery : IRequest<Result<GetDailyListsResponse>>
{
    public int Page { get; set; }
    public int UserId { get; set; }
    public DateTime? Date { get; set; }
    public string? Title { get; set; }
}