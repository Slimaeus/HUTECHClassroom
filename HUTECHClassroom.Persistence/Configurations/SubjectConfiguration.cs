using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Persistence.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(SubjectConstants.TITLE_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.TotalCredits)
            .HasDefaultValue(SubjectConstants.TOTAL_CREDITS_DEFAULT_VALUE);

        builder.Property(x => x.Code)
            .HasMaxLength(SubjectConstants.CODE_MAX_LENGTH);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.HasMany(x => x.Classrooms)
            .WithOne(x => x.Subject)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
