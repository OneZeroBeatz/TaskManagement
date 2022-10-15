using TaskManagement.Application.Messages.DailyLists;
using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Interfaces;

public interface IDailyListFactory
{
    DailyList CreateDailyList(CreateDailyListCommand request);
    DailyList CreateDailyList(UpdateDailyListCommand request);
}