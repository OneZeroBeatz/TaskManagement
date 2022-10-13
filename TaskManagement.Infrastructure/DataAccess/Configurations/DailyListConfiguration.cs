using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Models;

namespace TaskManagement.Infrastructure.DataAccess.Configurations;

public class DailyListConfiguration : IEntityTypeConfiguration<DailyList>
{
    public void Configure(EntityTypeBuilder<DailyList> builder)
    {
        builder.HasKey(it => it.Id);
        builder.Property(e => e.Date).HasColumnType("date");
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.Title).HasMaxLength(50);

        builder.HasOne(d => d.User)
            .WithMany(p => p.DailyLists)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
