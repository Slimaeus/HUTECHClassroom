using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.Property(x => x.Title)
            .IsRequired();

        builder.Property(x => x.TotalCredits)
            .HasDefaultValue(0);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.HasMany(x => x.Classrooms)
            .WithOne(x => x.Subject)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
