using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Persistence.Configurations;

public sealed class MajorConfiguration : IEntityTypeConfiguration<Major>
{
    public void Configure(EntityTypeBuilder<Major> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(MajorConstants.TITLE_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.TotalCredits)
            .HasDefaultValue(MajorConstants.TOTAL_CREDITS_DEFAULT_VALUE);

        builder.Property(x => x.NonComulativeCredits)
            .HasDefaultValue(MajorConstants.NON_COMULATIVE_CREDITS_DEFAULT_VALUE);

        builder.Property(x => x.Code)
            .HasMaxLength(MajorConstants.CODE_MAX_VALUE);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.HasMany(x => x.Subjects)
            .WithOne(x => x.Major)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
