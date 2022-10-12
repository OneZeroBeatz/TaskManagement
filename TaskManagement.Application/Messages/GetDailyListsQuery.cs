using MediatR;
using TaskManagement.Application.Messages.Responses;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages
{
    public class GetDailyListsQuery : IRequest<Result<GetDailyListsResponse>>
    {
        public int Page { get; set; }
        public string UserEmail { get; set; } = string.Empty;
    }
}