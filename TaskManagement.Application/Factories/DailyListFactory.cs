using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Messages.DailyLists;
using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Factories;

public class DailyListFactory : IDailyListFactory
{
    public DailyList CreateDailyList(CreateDailyListCommand request)
    {
        return new DailyList()
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            UserId = request.UserId
        };
    }

    public DailyList CreateDailyList(UpdateDailyListCommand request)
    {
        return new DailyList()
        {
            Id = request.DailyListId,
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            UserId = request.UserId
        };
    }
}
