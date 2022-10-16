using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Models;

namespace TaskManagement.Infrastructure.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(it => it.Id);
        builder.Property(e => e.Email).HasMaxLength(50);
        builder.Property(e => e.Password).HasMaxLength(50);
        builder.Property(e => e.TimezoneId).HasMaxLength(50);
        builder.HasIndex(e => e.Email).IsUnique();
        builder.HasData(new List<User>(3)
        {
            new User() {Id = 1, Email = "sasa.momcilovic94@gmail.com", Password = "user1pw", TimezoneId = "UTC" },
            new User() {Id = 2, Email = "user2email@gmail.com", Password = "user2pw", TimezoneId = "UTC" },
            new User() {Id = 3, Email = "user3email@gmail.com", Password = "user3pw", TimezoneId = "UTC" }
        });
    }
}
