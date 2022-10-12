using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Models;

namespace TaskManagement.Infrastructure.Repositories
{
    public class DailyListRepository : IDailyListRepository
    {
        public IEnumerable<DailyList> _dailyLists;

        public DailyListRepository()
        {
            _dailyLists = new List<DailyList>
            {
                new DailyList(){Id = 1, Description ="dailyListDescription1", Title = "dailyListTitle1", UserId = 1},
                new DailyList(){Id = 2, Description ="dailyListDescription2", Title = "dailyListTitle2", UserId = 1},
                new DailyList(){Id = 3, Description ="dailyListDescription3", Title = "dailyListTitle3", UserId = 1},
                new DailyList(){Id = 4, Description ="dailyListDescription4", Title = "dailyListTitle4", UserId = 2},
                new DailyList(){Id = 5, Description ="dailyListDescription5", Title = "dailyListTitle5", UserId = 2}
            };
        }

        public Task<List<DailyList>> Get(int page, int pageSize, int userId)
        {
            var dailyLists = _dailyLists.Where(x => x.UserId == userId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();

            return System.Threading.Tasks.Task.FromResult(dailyLists);
        }

        public Task<int> GetCountForUser(int userId)
        {
            var count = _dailyLists.Count(x => x.UserId == userId);

            return System.Threading.Tasks.Task.FromResult(count);
        }
    }
}
