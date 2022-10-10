using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Models;

namespace TaskManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> _users;

        public UserRepository()
        {
            _users = new List<User>()
            {
                new User(){ Id = 1, Email = "user1@gmail.com", Password = "user1password", Username = "user1username"},
                new User(){ Id = 2, Email = "user2@gmail.com", Password = "user2password", Username = "user2username"}
            };
        }

        public Task<User> GetByEmail(string email)
        {
            return System.Threading.Tasks.Task.FromResult(_users.First(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
