using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.DataAccess.Repositories.Base;

namespace TaskManagement.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken token = default);
        Task<string> GetTimezoneIdAsync(int userId, CancellationToken token = default);
        Task<List<User>> GetAllAsync(CancellationToken token = default);
    }
}