using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Models;

namespace TaskManagement.Infrastructure.Repositories
{
    public class DailyListRepository : IDailyListRepository
    {
        public List<DailyList> _dailyLists;

        public DailyListRepository()
        {
            _dailyLists = new List<DailyList>
            {
                new DailyList(){Id = 1, Description ="dailyListDescription1", Title = "dailyListTitle1", UserId = 1, Date = DateTime.UtcNow.Date},
                new DailyList(){Id = 2, Description ="dailyListDescription2", Title = "dailyListTitle2", UserId = 1, Date = DateTime.UtcNow.Date},
                new DailyList(){Id = 3, Description ="dailyListDescription3", Title = "dailyListTitle3", UserId = 1, Date = DateTime.UtcNow.Date.AddDays(1)},
                new DailyList(){Id = 4, Description ="dailyListDescription4", Title = "dailyListTitle4", UserId = 2, Date = DateTime.UtcNow.Date},
                new DailyList(){Id = 5, Description ="dailyListDescription5", Title = "dailyListTitle5", UserId = 2, Date = DateTime.UtcNow.Date}
            };
        }

        public System.Threading.Tasks.Task Create(DailyList dailyList)
        {
            dailyList.Id = _dailyLists.Max(x => x.Id) + 1;

            _dailyLists.Add(dailyList);
            return System.Threading.Tasks.Task.FromResult(true);
        }

        public Task<List<DailyList>> Get(int userId, DateTime date, string title, int page, int pageSize)
        {
            var dailyLists = _dailyLists
                .Where(x => x.UserId == userId)
                .Where(x => x.Date == date.Date)
                .Where(x => x.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();

            return System.Threading.Tasks.Task.FromResult(dailyLists);
        }

        public Task<int> GetCount(int userId, DateTime date, string title)
        {
            var count = _dailyLists
                .Where(x => x.UserId == userId)
                .Where(x => x.Date == date.Date)
                .Where(x => x.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .Count();

            return System.Threading.Tasks.Task.FromResult(count);
        }
    }
}
