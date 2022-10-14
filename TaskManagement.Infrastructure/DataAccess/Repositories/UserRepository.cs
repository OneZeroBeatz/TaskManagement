using Microsoft.EntityFrameworkCore;
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

        public Task<User?> GetByEmailAsync(string email)
        {
            return DbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }



        public async Task<string> GetTimezoneId(int userId)
        {
            return await DbContext.Users
                .Where(x => x.Id == userId)
                .Select(x => x.TimezoneId)
                .FirstAsync();
        }
    }
}
