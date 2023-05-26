using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(ProjectConstants.NAME_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(ProjectConstants.DESCRIPTION_MAX_LENGTH);

        builder.HasMany(x => x.Missions)
            .WithOne(x => x.Project)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
