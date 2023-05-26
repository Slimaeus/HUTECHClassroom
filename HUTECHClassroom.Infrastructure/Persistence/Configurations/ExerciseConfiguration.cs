using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(ExerciseConstants.TITLE_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Instruction)
            .HasMaxLength(ExerciseConstants.INSTRUCTION_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Link)
            .HasMaxLength(CommonConstants.LINK_MAX_LENGTH);

        builder.Property(x => x.TotalScore)
            .HasDefaultValue(ExerciseConstants.TOTAL_SCORE_DEFAULT_VALUE)
            .IsRequired();

        builder.Property(x => x.Deadline)
            .HasDefaultValue(DateTime.UtcNow.AddDays(1));

        builder.Property(x => x.Topic)
            .HasMaxLength(ExerciseConstants.TOPIC_MAX_LENGTH);

        builder.Property(x => x.Criteria)
            .HasMaxLength(ExerciseConstants.CRITERIA_MAX_LENGTH);

        builder.HasMany(x => x.Answers)
            .WithOne(x => x.Exercise)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
