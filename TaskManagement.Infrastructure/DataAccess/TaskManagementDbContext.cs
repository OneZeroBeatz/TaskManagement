using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.DataAccess.Configurations;

namespace TaskManagement.Infrastructure.DataAccess
{
    public partial class TaskManagementDbContext : DbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<DailyList> DailyLists { get; set; } = null!;
        public DbSet<Domain.Models.Task> Tasks { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DailyListConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            //var dailyLists = new List<DailyList>()
            //{
            //    new DailyList(){Id = 1, Description ="dailyListDescription1", Title = "dailyListTitle1", UserId = 1, Date = DateTime.UtcNow.Date},
            //    new DailyList(){Id = 2, Description ="dailyListDescription2", Title = "dailyListTitle2", UserId = 1, Date = DateTime.UtcNow.Date},
            //    new DailyList(){Id = 3, Description ="dailyListDescription3", Title = "dailyListTitle3", UserId = 1, Date = DateTime.UtcNow.Date.AddDays(1)},
            //    new DailyList(){Id = 4, Description ="dailyListDescription4", Title = "dailyListTitle4", UserId = 2, Date = DateTime.UtcNow.Date},
            //    new DailyList(){Id = 5, Description ="dailyListDescription5", Title = "dailyListTitle5", UserId = 2, Date = DateTime.UtcNow.Date}
            //};
            //
            //DailyLists.AddRange(dailyLists);
            //
            //var users = new List<User>()
            //{
            //    new User(){ Id = 1, Email = "user1@gmail.com", Password = "user1password"},
            //    new User(){ Id = 2, Email = "user2@gmail.com", Password = "user2password"}
            //};
            //
            //Users.AddRange(users);
        }
    }
}
