using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.Responses;
using TaskManagement.Application.Messages.Responses.Dtos;

namespace TaskManagement.Application.Factories
{
    public class GetDailyListsResponseFactory : IGetDailyListsResponseFactory
    {
        public GetDailyListsResponse GenerateResponse(int page, int pageCount, int pageSize, List<Domain.Models.DailyList> dailyListsForPage)
        {
            var dailyListDtos = new List<DailyListDto>(dailyListsForPage.Count);

            foreach (var item in dailyListsForPage)
            {
                var dailyListDto = new DailyListDto(item.Id, item.Title, item.Description, item.Date);
                dailyListDtos.Add(dailyListDto);
            }

            return new GetDailyListsResponse()
            {
                DailyLists = dailyListDtos,
                Page = page,
                PageCount = pageCount,
                PageSize = pageSize
            };
        }
    }
}
