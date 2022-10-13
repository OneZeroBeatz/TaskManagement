using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.DataAccess;

namespace TaskManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskManagementDbContext _dbContext;

        public UserRepository(TaskManagementDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<User?> GetById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
        public Task<User?> GetByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async System.Threading.Tasks.Task UpdateTimezone(string timeZoneId, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            user!.TimeZoneId = timeZoneId;

            _dbContext.Update(user);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetTimezoneId(int userId)
        {
            return await _dbContext.Users
                .Where(x => x.Id == userId)
                .Select(x => x.TimeZoneId)
                .FirstAsync();
        }
    }
}
