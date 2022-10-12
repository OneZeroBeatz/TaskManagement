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
        builder.Property(e => e.TimeZoneId).HasMaxLength(50);
        builder.HasIndex(e => e.Email, "UNC_Users_Email")
                            .IsUnique();
    }
}
