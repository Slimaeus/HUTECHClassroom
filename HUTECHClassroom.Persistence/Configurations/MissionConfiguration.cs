using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Persistence.Configurations;

public sealed class MissionConfiguration : IEntityTypeConfiguration<Mission>
{
    public void Configure(EntityTypeBuilder<Mission> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(MissionConstants.TITLE_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(MissionConstants.DESCRIPTION_MAX_LENGTH);
    }
}
