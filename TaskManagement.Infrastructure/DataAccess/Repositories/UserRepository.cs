using Microsoft.EntityFrameworkCore;
using System.Threading;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.DataAccess;
using TaskManagement.Infrastructure.DataAccess.Repositories.Base;

namespace TaskManagement.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TaskManagementDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<User>> GetAllAsync(CancellationToken token = default)
        {
            return DbContext.Users.ToListAsync(token);
        }

        public Task<User?> GetByEmailAsync(string email, CancellationToken token)
        {
            return DbContext.Users
                .FirstOrDefaultAsync(x => x.Email.Equals(email), token);
        }
        public Task<string> GetTimezoneIdAsync(int userId, CancellationToken token)
        {
            return DbContext.Users
                .Where(x => x.Id == userId)
                .Select(x => x.TimezoneId)
                .FirstAsync(token);
        }
    }
}
