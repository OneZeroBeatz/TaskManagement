using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Repositories
{
    //TODO: Consider cancelation token adding
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        System.Threading.Tasks.Task UpdateTimezone(string timeZoneId, string email);
    }
}