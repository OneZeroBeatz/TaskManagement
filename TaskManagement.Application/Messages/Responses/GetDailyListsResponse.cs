using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Messages.Responses
{
    public class GetDailyListsResponse
    {
        public int Page { get; set; }
        public int PageCount { get; set; }
        //TODO: Consider using different DailyList class for response, adapted for this usage
        public List<DailyList> DailyLists { get; set; }
    }
}