using TaskManagement.Application.Messages.Responses.Dtos;

namespace TaskManagement.Application.Messages.Responses
{
    public class GetDailyListsResponse
    {
        public int Page { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public List<DailyListDto> DailyLists { get; set; } = new();
    }
}