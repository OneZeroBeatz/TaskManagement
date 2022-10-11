using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmail(string email);
    }
}