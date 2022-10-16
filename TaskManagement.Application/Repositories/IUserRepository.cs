using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.DataAccess.Repositories.Base;

namespace TaskManagement.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken token);
        Task<string> GetTimezoneIdAsync(int userId, CancellationToken token);
        Task<List<User>> GetAllAsync();
    }
}