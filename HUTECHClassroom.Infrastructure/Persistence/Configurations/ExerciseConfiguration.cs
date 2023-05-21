using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Instruction)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Link)
            .HasMaxLength(200);

        builder.Property(x => x.TotalScore)
            .IsRequired();

        builder.Property(x => x.Deadline)
            .HasDefaultValue(DateTime.UtcNow.AddDays(1));

        builder.Property(x => x.Topic)
            .HasMaxLength(20);

        builder.Property(x => x.Criteria)
            .HasMaxLength(200);

        builder.HasMany(x => x.Answers)
            .WithOne(x => x.Exercise)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
