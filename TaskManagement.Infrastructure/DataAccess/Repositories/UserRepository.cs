﻿using Microsoft.EntityFrameworkCore;
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

        public Task<User?> GetByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async System.Threading.Tasks.Task UpdateTimezone(string timeZoneId, string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
            user!.TimeZoneId = timeZoneId;

            _dbContext.Update(user);

            await _dbContext.SaveChangesAsync();
        }
    }
}