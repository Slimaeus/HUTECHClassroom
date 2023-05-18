using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class MajorConfiguration : IEntityTypeConfiguration<Major>
{
    public void Configure(EntityTypeBuilder<Major> builder)
    {
        builder.Property(x => x.Title)
            .IsRequired();

        builder.Property(x => x.TotalCredits)
            .HasDefaultValue(0);

        builder.Property(x => x.NonComulativeCredits)
            .HasDefaultValue(0);
    }
}
