using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Infrastructure.DataAccess.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Domain.Models.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Task> builder)
    {
        builder.HasKey(it => it.Id);
        builder.Property(e => e.Deadline);
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.Title).HasMaxLength(50);

        //builder.HasOne(d => d.DailyList)
        //    .WithMany(p => p.Tasks)
        //    .HasForeignKey(d => d.DailyListId)
        //    .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
