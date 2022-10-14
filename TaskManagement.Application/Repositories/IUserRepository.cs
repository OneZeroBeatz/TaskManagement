using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.DataAccess.Repositories.Base;

namespace TaskManagement.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<string> GetTimezoneId(int userId);
        Task<List<User>> GetAll();
    }
}