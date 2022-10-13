using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        System.Threading.Tasks.Task UpdateTimezone(string timeZoneId, int userId);
    }
}